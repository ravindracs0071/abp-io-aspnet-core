using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Settings;

namespace Sample.Demo.PropertySetting
{
    public interface IPropertySettingManagementStore
    {
        Task<string> GetOrNullAsync(string name, string providerName, string providerKey);

        Task<List<PropertySettingValue>> GetListAsync(string providerName, string providerKey);

        Task<List<PropertySettingValue>> GetListAsync(string[] names, string providerName, string providerKey);

        Task SetAsync(string name, string value, string providerName, string providerKey);

        Task DeleteAsync(string name, string providerName, string providerKey);
    }
}
