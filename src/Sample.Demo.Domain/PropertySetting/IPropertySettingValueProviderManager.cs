using System.Collections.Generic;

namespace Sample.Demo.PropertySetting
{
    public interface IPropertySettingValueProviderManager
    {
        List<IPropertySettingValueProvider> Providers { get; }
    }
}