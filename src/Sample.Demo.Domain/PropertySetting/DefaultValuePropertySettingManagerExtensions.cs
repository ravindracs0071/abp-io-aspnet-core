using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Settings;

namespace Sample.Demo.PropertySetting
{
    public static class DefaultValuePropertySettingManagerExtensions
    {
        public static Task<string> GetOrNullDefaultAsync(this IPropertySettingManager propertySettingManager, [NotNull] string name, bool fallback = true)
        {
            return propertySettingManager.GetOrNullAsync(name, DefaultValueSettingValueProvider.ProviderName, null, fallback);
        }

        public static Task<List<PropertySettingValue>> GetAllDefaultAsync(this IPropertySettingManager propertySettingManager, bool fallback = true)
        {
            return propertySettingManager.GetAllAsync(DefaultValueSettingValueProvider.ProviderName, null, fallback);
        }
    }
}