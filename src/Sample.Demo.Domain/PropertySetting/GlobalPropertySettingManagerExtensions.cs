using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Settings;

namespace Sample.Demo.PropertySetting
{
    public static class GlobalPropertySettingManagerExtensions
    {
        public static Task<string> GetOrNullGlobalAsync(this IPropertySettingManager settingManager, [NotNull] string name, bool fallback = true)
        {
            return settingManager.GetOrNullAsync(name, GlobalSettingValueProvider.ProviderName, null, fallback);
        }

        public static Task<List<PropertySettingValue>> GetAllGlobalAsync(this IPropertySettingManager settingManager, bool fallback = true)
        {
            return settingManager.GetAllAsync(GlobalSettingValueProvider.ProviderName, null, fallback);
        }

        public static Task SetGlobalAsync(this IPropertySettingManager settingManager, [NotNull] string name, [CanBeNull] string value, bool visible, bool requiredRegEx, [CanBeNull] string regExRule)
        {
            return settingManager.SetAsync(name, value, visible, requiredRegEx, GlobalSettingValueProvider.ProviderName, null, regExRule);
        }
    }
}
