using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Settings;

namespace Sample.Demo.PropertySetting
{
    public interface IPropertySettingStore
    {
        Task<string> GetOrNullAsync(string name, string providerName, string providerKey);

        Task<List<PropertySettingValue>> GetListAsync(string providerName, string providerKey);

        Task<List<PropertySettingValue>> GetListAsync(string[] names, string providerName, string providerKey);

        Task SetAsync(string name, string value, bool visible, bool requiredRegEx, string providerName, string providerKey, string regExRule);

        Task DeleteAsync(string name, string providerName, string providerKey);
    }
}
