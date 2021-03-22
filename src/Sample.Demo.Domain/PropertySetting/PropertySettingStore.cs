using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;

namespace Sample.Demo.PropertySetting
{
    public class PropertySettingStore : IPropertySettingStore, ITransientDependency
    {
        protected IPropertySettingManagementStore ManagementStore { get; }

        public PropertySettingStore(IPropertySettingManagementStore managementStore)
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
    }
}
