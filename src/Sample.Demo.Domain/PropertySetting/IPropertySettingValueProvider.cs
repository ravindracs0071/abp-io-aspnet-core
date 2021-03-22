using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Sample.Demo.PropertySetting
{
    public interface IPropertySettingValueProvider
    {
        string Name { get; }

        Task<string> GetOrNullAsync([NotNull] PropertySettingDefinition setting);

        Task<List<PropertySettingValue>> GetAllAsync([NotNull] PropertySettingDefinition[] settings);
    }
}
