using Volo.Abp.Collections;

namespace Sample.Demo.PropertySetting
{
    public class PropertySettingOptions
    {
        public ITypeList<IPropertySettingProvider> Providers { get; }

        public PropertySettingOptions()
        {
            Providers = new TypeList<IPropertySettingProvider>();
        }
    }
}
