using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Settings;
using Volo.Abp.Uow;

namespace Sample.Demo.PropertySetting
{
    public class PropertySettingManagementStore : IPropertySettingManagementStore, ITransientDependency
    {
        protected IDistributedCache<PropertySettingCacheItem> Cache { get; }
        protected IPropertySettingRepository SettingRepository { get; }
        protected IGuidGenerator GuidGenerator { get; }
        protected ICurrentTenant CurrentTenant { get; }

        public PropertySettingManagementStore(
            IPropertySettingRepository settingRepository,
            IGuidGenerator guidGenerator,
            IDistributedCache<PropertySettingCacheItem> cache,
            ICurrentTenant currentTenant)
        {
            SettingRepository = settingRepository;
            GuidGenerator = guidGenerator;
            Cache = cache;
            CurrentTenant = currentTenant;
        }

        [UnitOfWork]
        public virtual async Task<string> GetOrNullAsync(string name, string providerName, string providerKey)
        {
            return (await GetCacheItemAsync(name, providerName, providerKey)).Value;
        }

        [UnitOfWork]
        public virtual async Task SetAsync(string name, string value, string providerName, string providerKey)
        {
            var setting = await SettingRepository.FindAsync(name, providerName, providerKey);
            if (setting == null)
            {
                setting = new PropertySetting(GuidGenerator.Create(), name, value, providerName, providerKey);
                await SettingRepository.InsertAsync(setting);
            }
            else
            {
                setting.Value = value;
                await SettingRepository.UpdateAsync(setting);
            }

            await Cache.SetAsync(CalculateCacheKey(CurrentTenant.Id, name, providerName, providerKey), new PropertySettingCacheItem(setting?.Value, setting.Visible, setting.RequiredRegEx, setting?.RegExRule), considerUow: true);
        }

        public virtual async Task<List<PropertySettingValue>> GetListAsync(string providerName, string providerKey)
        {
            var settings = await SettingRepository.GetListAsync(providerName, providerKey);
            return settings.Select(s => new PropertySettingValue(s.Name, s.Value)).ToList();
        }

        [UnitOfWork]
        public virtual async Task DeleteAsync(string name, string providerName, string providerKey)
        {
            var setting = await SettingRepository.FindAsync(name, providerName, providerKey);
            if (setting != null)
            {
                await SettingRepository.DeleteAsync(setting);
                await Cache.RemoveAsync(CalculateCacheKey(CurrentTenant.Id, name, providerName, providerKey), considerUow: true);
            }
        }

        protected virtual async Task<PropertySettingCacheItem> GetCacheItemAsync(string name, string providerName, string providerKey)
        {
            var cacheKey = CalculateCacheKey(CurrentTenant.Id, name, providerName, providerKey);
            var cacheItem = await Cache.GetAsync(cacheKey, considerUow: true);

            if (cacheItem != null)
            {
                return cacheItem;
            }

            cacheItem = new PropertySettingCacheItem(null, true, false, null);

            await SetCacheItemsAsync(providerName, providerKey, name, cacheItem);

            return cacheItem;
        }

        private async Task SetCacheItemsAsync(
            string providerName,
            string providerKey,
            string currentName,
            PropertySettingCacheItem currentCacheItem)
        {
            var settingsDictionary = (await SettingRepository.GetListAsync(providerName, providerKey));

            var cacheItems = new List<KeyValuePair<string, PropertySettingCacheItem>>();

            foreach (var dic in settingsDictionary)
            {
                cacheItems.Add(
                    new KeyValuePair<string, PropertySettingCacheItem>(
                        CalculateCacheKey(dic.TenantId, dic.Name, dic.ProviderName, dic.ProviderKey),
                        new PropertySettingCacheItem(dic.Value, dic.Visible, dic.RequiredRegEx, dic.RegExRule)
                    )
                );
            }

            await Cache.SetManyAsync(cacheItems, considerUow: true);
        }

        [UnitOfWork]
        public async Task<Dictionary<string, PropertySettingValue>> GetListAsync(string[] names, string providerName, string providerKey)
        {
            Check.NotNullOrEmpty(names, nameof(names));

            var result = new Dictionary<string, PropertySettingValue>();

            if (names.Length == 1)
            {
                var name = names.First();
                result.Add(name, new PropertySettingValue(name, (await GetCacheItemAsync(name, providerName, providerKey)).Value));
                return result;
            }

            var cacheItems = await GetCacheItemsAsync(names, providerName, providerKey);
            foreach (var item in cacheItems)
            {
                string value = null, regExRule = null;
                bool visible = true, requiredRegEx = false;
                if (item.Value != null) 
                {
                    value = item.Value?.Value;
                    regExRule = item.Value?.RegExRule;
                    visible = item.Value.Visible;
                    requiredRegEx = item.Value.RequiredRegEx;
                }
                string keyName = GetSettingNameFormCacheKeyOrNull(item.Key);
                result.Add(keyName, new PropertySettingValue(keyName, value, visible, requiredRegEx, regExRule));
            }

            return result;
        }

        protected virtual async Task<List<KeyValuePair<string, PropertySettingCacheItem>>> GetCacheItemsAsync(string[] names, string providerName, string providerKey)
        {
            var cacheKeys = names.Select(x => CalculateCacheKey(CurrentTenant.Id, x, providerName, providerKey)).ToList();

            var cacheItems = (await Cache.GetManyAsync(cacheKeys, considerUow: true)).ToList();

            if (cacheItems.All(x => x.Value != null))
            {
                return cacheItems;
            }

            var notCacheKeys = cacheItems.Where(x => x.Value == null).Select(x => x.Key).ToList();

            var newCacheItems = await SetCacheItemsAsync(providerName, providerKey, notCacheKeys);

            var result = new List<KeyValuePair<string, PropertySettingCacheItem>>();
            foreach (var key in cacheKeys)
            {
                var item = newCacheItems.FirstOrDefault(x => x.Key == key);
                if (item.Value == null)
                {
                    item = cacheItems.FirstOrDefault(x => x.Key == key);
                }

                result.Add(new KeyValuePair<string, PropertySettingCacheItem>(key, item.Value));
            }

            return result;
        }

        private async Task<List<KeyValuePair<string, PropertySettingCacheItem>>> SetCacheItemsAsync(
            string providerName,
            string providerKey,
            List<string> notCacheKeys)
        {
            var settingsDictionary = (await SettingRepository.GetListAsync(notCacheKeys.Select(GetSettingNameFormCacheKeyOrNull).ToArray(), providerName, providerKey));

            var cacheItems = new List<KeyValuePair<string, PropertySettingCacheItem>>();

            foreach (var dic in settingsDictionary) 
            {
                cacheItems.Add(
                    new KeyValuePair<string, PropertySettingCacheItem>(
                        CalculateCacheKey(dic.TenantId, dic.Name, dic.ProviderName, dic.ProviderKey),
                        new PropertySettingCacheItem(dic.Value, dic.Visible, dic.RequiredRegEx, dic.RegExRule)
                    )
                );
            }

            await Cache.SetManyAsync(cacheItems, considerUow: true);

            return cacheItems;
        }


        protected virtual string CalculateCacheKey(Guid? tenantId, string name, string providerName, string providerKey)
        {
            return PropertySettingCacheItem.CalculateCacheKey(tenantId, name, providerName, providerKey);
        }

        protected virtual string GetSettingNameFormCacheKeyOrNull(string key)
        {
            //TODO: throw ex when name is null?
            return PropertySettingCacheItem.GetSettingNameFormCacheKeyOrNull(key);
        }
    }
}
