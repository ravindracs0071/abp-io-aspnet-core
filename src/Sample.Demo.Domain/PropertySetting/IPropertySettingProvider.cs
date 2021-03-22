using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Sample.Demo.PropertySetting
{
    public interface IPropertySettingProvider
    {
        Task<string> GetOrNullAsync([NotNull]string name);

        Task<List<PropertySettingValue>> GetAllAsync([NotNull]string[] names);

        Task<List<PropertySettingValue>> GetAllAsync();
    }
}
