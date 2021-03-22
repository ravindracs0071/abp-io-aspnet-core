using System;
using Volo.Abp;

namespace Sample.Demo.PropertySetting
{
    [Serializable]
    public class PropertySettingValue : NameValue
    {
        public bool Visible { get; set; }

        public bool RequiredRegEx { get; set; }

        public string RegExRule { get; set; }

        public PropertySettingValue()
        {

        }

        public PropertySettingValue(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public PropertySettingValue(string name, string value, bool visible, bool requiredRegEx, string regExRule)
        {
            Name = name;
            Value = value;
            Visible = visible;
            RequiredRegEx = requiredRegEx;
            RegExRule = regExRule;
        }
    }
}
