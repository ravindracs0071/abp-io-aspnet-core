using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;

namespace Sample.Demo.PropertySetting
{
    public class GlobalPropertySettingProvider : PropertySettingProvider, ITransientDependency
    {
        public override string Name => GlobalSettingValueProvider.ProviderName;

        public GlobalPropertySettingProvider(IPropertySettingStore PropertySettingStore) 
            : base(PropertySettingStore)
        {

        }

        protected override string NormalizeProviderKey(string providerKey)
        {
            return null;
        }
    }
}