using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Settings;

namespace Sample.Demo.PropertySetting
{
    public static class ConfigurationValuePropertySettingManagerExtensions
    {
        public static Task<string> GetOrNullConfigurationAsync(this IPropertySettingManager propertySettingManager, [NotNull] string name, bool fallback = true)
        {
            return propertySettingManager.GetOrNullAsync(name, ConfigurationSettingValueProvider.ProviderName, null, fallback);
        }

        public static Task<List<PropertySettingValue>> GetAllConfigurationAsync(this IPropertySettingManager propertySettingManager, bool fallback = true)
        {
            return propertySettingManager.GetAllAsync(ConfigurationSettingValueProvider.ProviderName, null, fallback);
        }
    }
}