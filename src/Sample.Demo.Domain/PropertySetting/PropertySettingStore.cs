using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Settings;
using Volo.Abp.Uow;

namespace Sample.Demo.PropertySetting
{
    public class PropertySettingStore : IPropertySettingStore, ITransientDependency
    {
        protected IDistributedCache<PropertySettingCacheItem> Cache { get; }
        protected ISettingDefinitionManager SettingDefinitionManager { get; }
        protected IPropertySettingRepository SettingRepository { get; }
        protected IGuidGenerator GuidGenerator { get; }

        public PropertySettingStore(
            IPropertySettingRepository settingRepository,
            IGuidGenerator guidGenerator,
            IDistributedCache<PropertySettingCacheItem> cache,
            ISettingDefinitionManager settingDefinitionManager)
        {
            SettingRepository = settingRepository;
            GuidGenerator = guidGenerator;
            Cache = cache;
            SettingDefinitionManager = settingDefinitionManager;
        }

        [UnitOfWork]
        public virtual async Task<string> GetOrNullAsync(string name, string providerName, string providerKey)
        {
            return (await GetCacheItemAsync(name, providerName, providerKey)).Value;
        }

        [UnitOfWork]
        public virtual async Task SetAsync(string name, string value, bool visible, bool requiredRegEx, string providerName, string providerKey, string regExRule)
        {
            var setting = await SettingRepository.FindAsync(name, providerName, providerKey);
            if (setting == null)
            {
                setting = new PropertySetting(GuidGenerator.Create(), name, value, visible, requiredRegEx, providerName, providerKey, regExRule);
                await SettingRepository.InsertAsync(setting);
            }
            else
            {
                setting.Value = value;
                setting.Visible = visible;
                setting.RequiredRegEx = requiredRegEx;
                setting.RegExRule = regExRule;
                await SettingRepository.UpdateAsync(setting);
            }

            await Cache.SetAsync(CalculateCacheKey(name, providerName, providerKey), new PropertySettingCacheItem(setting?.Value), considerUow: true);
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
                await Cache.RemoveAsync(CalculateCacheKey(name, providerName, providerKey), considerUow: true);
            }
        }

        protected virtual async Task<PropertySettingCacheItem> GetCacheItemAsync(string name, string providerName, string providerKey)
        {
            var cacheKey = CalculateCacheKey(name, providerName, providerKey);
            var cacheItem = await Cache.GetAsync(cacheKey, considerUow: true);

            if (cacheItem != null)
            {
                return cacheItem;
            }

            cacheItem = new PropertySettingCacheItem(null);

            await SetCacheItemsAsync(providerName, providerKey, name, cacheItem);

            return cacheItem;
        }

        private async Task SetCacheItemsAsync(
            string providerName,
            string providerKey,
            string currentName,
            PropertySettingCacheItem currentCacheItem)
        {
            var settingDefinitions = SettingDefinitionManager.GetAll();
            var settingsDictionary = (await SettingRepository.GetListAsync(providerName, providerKey))
                .ToDictionary(s => s.Name, s => s.Value);

            var cacheItems = new List<KeyValuePair<string, PropertySettingCacheItem>>();

            foreach (var settingDefinition in settingDefinitions)
            {
                var settingValue = settingsDictionary.GetOrDefault(settingDefinition.Name);

                cacheItems.Add(
                    new KeyValuePair<string, PropertySettingCacheItem>(
                        CalculateCacheKey(settingDefinition.Name, providerName, providerKey),
                        new PropertySettingCacheItem(settingValue)
                    )
                );

                if (settingDefinition.Name == currentName)
                {
                    currentCacheItem.Value = settingValue;
                }
            }

            await Cache.SetManyAsync(cacheItems, considerUow: true);
        }

        private async Task SetCacheItemsAsync(
            string providerName,
            string providerKey,
            PropertySettingCacheItem currentCacheItem)
        {
            var settingDefinitions = SettingDefinitionManager.GetAll();
            var settingsDictionary = (await SettingRepository.GetListAsync(providerName, providerKey))
                .ToDictionary(s => s.Name, s => s.Value);

            var cacheItems = new List<KeyValuePair<string, PropertySettingCacheItem>>();

            foreach (var settingDefinition in settingDefinitions)
            {
                var settingValue = settingsDictionary.GetOrDefault(settingDefinition.Name);

                cacheItems.Add(
                    new KeyValuePair<string, PropertySettingCacheItem>(
                        CalculateCacheKey(settingDefinition.Name, providerName, providerKey),
                        new PropertySettingCacheItem(settingValue)
                    )
                );

                //if (settingDefinition.Name == currentName)
                //{
                //    currentCacheItem.Value = settingValue;
                //}
            }

            await Cache.SetManyAsync(cacheItems, considerUow: true);
        }

        [UnitOfWork]
        public async Task<List<PropertySettingValue>> GetListAsync(string[] names, string providerName, string providerKey)
        {
            Check.NotNullOrEmpty(names, nameof(names));

            var result = new List<PropertySettingValue>();

            if (names.Length == 1)
            {
                var name = names.First();
                result.Add(new PropertySettingValue(name, (await GetCacheItemAsync(name, providerName, providerKey)).Value));
                return result;
            }

            var cacheItems = await GetCacheItemsAsync(names, providerName, providerKey);
            foreach (var item in cacheItems)
            {
                result.Add(new PropertySettingValue(GetSettingNameFormCacheKeyOrNull(item.Key), item.Value?.Value));
            }

            return result;
        }

        protected virtual async Task<List<KeyValuePair<string, PropertySettingCacheItem>>> GetCacheItemsAsync(string[] names, string providerName, string providerKey)
        {
            var cacheKeys = names.Select(x => CalculateCacheKey(x, providerName, providerKey)).ToList();

            var cacheItems = (await Cache.GetManyAsync(cacheKeys, considerUow: true)).ToList();

            if (cacheItems.All(x => x.Value != null))
            {
                return cacheItems;
            }

            var notCacheKeys = cacheItems.Where(x => x.Value == null).Select(x => x.Key).ToList();

            var newCacheItems = await SetCacheItemsAsync(providerName, providerKey, null, notCacheKeys);

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
            string name,
            List<string> notCacheKeys)
        {
            var settingDefinitions = SettingDefinitionManager.GetAll().Where(x => notCacheKeys.Any(k => GetSettingNameFormCacheKeyOrNull(k) == x.Name));

            var settingsDictionary = (await SettingRepository.GetListAsync(notCacheKeys.Select(GetSettingNameFormCacheKeyOrNull).ToArray(), providerName, providerKey))
                .ToDictionary(s => s.Name, s => s.Value);

            var cacheItems = new List<KeyValuePair<string, PropertySettingCacheItem>>();

            foreach (var settingDefinition in settingDefinitions)
            {
                var settingValue = settingsDictionary.GetOrDefault(settingDefinition.Name);
                cacheItems.Add(
                    new KeyValuePair<string, PropertySettingCacheItem>(
                        CalculateCacheKey(settingDefinition.Name, providerName, providerKey),
                        new PropertySettingCacheItem(settingValue)
                    )
                );
            }

            await Cache.SetManyAsync(cacheItems, considerUow: true);

            return cacheItems;
        }


        protected virtual string CalculateCacheKey(string name, string providerName, string providerKey)
        {
            return PropertySettingCacheItem.CalculateCacheKey(name, providerName, providerKey);
        }

        protected virtual string GetSettingNameFormCacheKeyOrNull(string key)
        {
            //TODO: throw ex when name is null?
            return PropertySettingCacheItem.GetSettingNameFormCacheKeyOrNull(key);
        }
    }
}
