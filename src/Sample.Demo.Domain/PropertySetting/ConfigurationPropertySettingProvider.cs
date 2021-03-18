using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;

namespace Sample.Demo.PropertySetting
{
    public class ConfigurationPropertySettingProvider : IPropertySettingProvider, ITransientDependency
    {
        public string Name => ConfigurationSettingValueProvider.ProviderName;

        protected IConfiguration Configuration { get; }

        public ConfigurationPropertySettingProvider(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public virtual Task<string> GetOrNullAsync(SettingDefinition setting, string providerKey)
        {
            return Task.FromResult(Configuration[ConfigurationSettingValueProvider.ConfigurationNamePrefix + setting.Name]);
        }

        public virtual Task SetAsync(SettingDefinition setting, string name, string value, bool visible, bool requiredRegEx, string providerName, string providerKey, string regExRule)
        {
            throw new AbpException($"Can not set a setting value to the application configuration.");
        }

        public virtual Task ClearAsync(SettingDefinition setting, string providerKey)
        {
            throw new AbpException($"Can not set a setting value to the application configuration.");
        }
    }
}