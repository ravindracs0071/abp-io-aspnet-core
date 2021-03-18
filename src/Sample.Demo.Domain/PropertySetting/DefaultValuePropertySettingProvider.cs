using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;

namespace Sample.Demo.PropertySetting
{
    public class DefaultValuePropertySettingProvider : IPropertySettingProvider, ISingletonDependency
    {
        public string Name => DefaultValueSettingValueProvider.ProviderName;

        public virtual Task<string> GetOrNullAsync(SettingDefinition setting, string providerKey)
        {
            return Task.FromResult(setting.DefaultValue);
        }

        public virtual Task SetAsync(SettingDefinition setting, string name, string value, bool visible, bool requiredRegEx, string providerName, string providerKey, string regExRule)
        {
            throw new AbpException($"Can not set default value of a setting. It is only possible while defining the setting in a {typeof(ISettingDefinitionProvider)} implementation.");
        }

        public virtual Task ClearAsync(SettingDefinition setting, string providerKey)
        {
            throw new AbpException($"Can not clear default value of a setting. It is only possible while defining the setting in a {typeof(ISettingDefinitionProvider)} implementation.");
        }
    }
}