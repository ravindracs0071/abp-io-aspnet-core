using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Volo.Abp.DependencyInjection;

namespace Sample.Demo.PropertySetting
{
    [Dependency(TryRegister = true)]
    public class NullPropertySettingStore : IPropertySettingStore, ISingletonDependency
    {
        public ILogger<NullPropertySettingStore> Logger { get; set; }

        public NullPropertySettingStore()
        {
            Logger = NullLogger<NullPropertySettingStore>.Instance;
        }

        public Task<string> GetOrNullAsync(string name, string providerName, string providerKey)
        {
            return Task.FromResult((string) null);
        }

        public Task<List<PropertySettingValue>> GetAllAsync(string[] names, string providerName, string providerKey)
        {
            return Task.FromResult(names.Select(x => new PropertySettingValue(x, null)).ToList());
        }
    }
}
