using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Sample.Demo.PropertySetting
{
    public interface IPropertySettingStore
    {
        Task<string> GetOrNullAsync(
            [NotNull] string name,
            [CanBeNull] string providerName,
            [CanBeNull] string providerKey
        );

        Task<List<PropertySettingValue>> GetAllAsync(
            [NotNull] string[] names,
            [CanBeNull] string providerName,
            [CanBeNull] string providerKey
        );
    }
}
