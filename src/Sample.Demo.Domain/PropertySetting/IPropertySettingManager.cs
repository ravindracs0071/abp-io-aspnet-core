using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Settings;

namespace Sample.Demo.PropertySetting
{
    public interface IPropertySettingManager
    {
        Task<string> GetOrNullAsync([NotNull]string name, [NotNull] string providerName, [CanBeNull] string providerKey, bool fallback = true);

        Task<List<PropertySettingValue>> GetAllAsync([NotNull] string providerName, [CanBeNull] string providerKey, bool fallback = true);

        Task SetAsync([NotNull] string name, [CanBeNull] string value, bool visible, bool requiredRegEx, [NotNull] string providerName, [CanBeNull] string providerKey, string regExRule, bool forceToSet = false);
    }
}