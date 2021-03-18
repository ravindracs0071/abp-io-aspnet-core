using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Settings;

namespace Sample.Demo.PropertySetting
{
    public interface IPropertySettingProvider 
    {
        string Name { get; }

        Task<string> GetOrNullAsync([NotNull] SettingDefinition setting, [CanBeNull] string providerKey);

        Task SetAsync([NotNull] SettingDefinition setting, [NotNull] string name, [NotNull] string value, bool visible, bool requiredRegEx, [CanBeNull] string providerName, [CanBeNull] string providerKey, [CanBeNull] string regExRule);

        Task ClearAsync([NotNull] SettingDefinition setting, [CanBeNull] string providerKey);
    }
}
