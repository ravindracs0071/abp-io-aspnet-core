using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;

namespace Sample.Demo.PropertySetting
{
    public class PropertySettingManagementStore : IPropertySettingStore, ITransientDependency
    {
        protected IPropertySettingStore ManagementStore { get; }

        public PropertySettingManagementStore(IPropertySettingStore managementStore)
        {
            ManagementStore = managementStore;
        }

        public virtual Task<string> GetOrNullAsync(string name, string providerName, string providerKey)
        {
            return ManagementStore.GetOrNullAsync(name, providerName, providerKey);
        }

        public virtual Task<List<PropertySettingValue>> GetAllAsync(string[] names, string providerName, string providerKey)
        {
            return ManagementStore.GetListAsync(names, providerName, providerKey);
        }

        public Task<List<PropertySettingValue>> GetListAsync(string providerName, string providerKey)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<PropertySettingValue>> GetListAsync(string[] names, string providerName, string providerKey)
        {
            throw new System.NotImplementedException();
        }

        public Task SetAsync(string name, string value, bool visible, bool requiredRegEx, string providerName, string providerKey, string regExRule)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(string name, string providerName, string providerKey)
        {
            throw new System.NotImplementedException();
        }
    }
}
