using System.Threading.Tasks;
using Volo.Abp.Settings;

namespace Sample.Demo.PropertySetting
{
    public abstract class PropertySettingProvider : IPropertySettingProvider
    {
        public abstract string Name { get; }

        //TODO: Rename to Store
        protected IPropertySettingStore PropertySettingStore { get; }

        protected PropertySettingProvider(IPropertySettingStore PropertySettingStore)
        {
            this.PropertySettingStore = PropertySettingStore;
        }

        public virtual async Task<string> GetOrNullAsync(SettingDefinition setting, string providerKey)
        {
            return await PropertySettingStore.GetOrNullAsync(setting.Name, Name, NormalizeProviderKey(providerKey));
        }

        public virtual async Task SetAsync(SettingDefinition setting, string name, string value, bool visible, bool requiredRegEx, string providerName, string providerKey, string regExRule)
        {
            await PropertySettingStore.SetAsync(setting.Name, value, visible, requiredRegEx, providerName, NormalizeProviderKey(providerKey), regExRule);
        }

        public virtual async Task ClearAsync(SettingDefinition setting, string providerKey)
        {
            await PropertySettingStore.DeleteAsync(setting.Name, Name, NormalizeProviderKey(providerKey));
        }

        protected virtual string NormalizeProviderKey(string providerKey)
        {
            return providerKey;
        }
    }
}