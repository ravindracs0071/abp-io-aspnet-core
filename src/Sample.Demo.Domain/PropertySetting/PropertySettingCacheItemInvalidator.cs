using System;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;

namespace Sample.Demo.PropertySetting
{
    public class PropertySettingCacheItemInvalidator : ILocalEventHandler<EntityChangedEventData<PropertySetting>>, ITransientDependency
    {
        protected IDistributedCache<PropertySettingCacheItem> Cache { get; }

        public PropertySettingCacheItemInvalidator(IDistributedCache<PropertySettingCacheItem> cache)
        {
            Cache = cache;
        }

        public virtual async Task HandleEventAsync(EntityChangedEventData<PropertySetting> eventData)
        {
            var cacheKey = CalculateCacheKey(
                eventData.Entity.TenantId,
                eventData.Entity.Name,
                eventData.Entity.ProviderName,
                eventData.Entity.ProviderKey
            );

            await Cache.RemoveAsync(cacheKey);
        }

        protected virtual string CalculateCacheKey(Guid? tenantId, string name, string providerName, string providerKey)
        {
            return PropertySettingCacheItem.CalculateCacheKey(tenantId, name, providerName, providerKey);
        }
    }
}
