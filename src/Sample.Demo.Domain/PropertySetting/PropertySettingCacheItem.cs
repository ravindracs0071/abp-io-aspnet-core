using System;
using System.Linq;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Text.Formatting;

namespace Sample.Demo.PropertySetting
{
    [Serializable]
    public class PropertySettingCacheItem
    {
        private const string CacheKeyFormat = "t:{0}pn:{1},pk:{2},n:{3}";

        public string Value { get; set; }
        public bool Visible { get; set; }
        public bool RequiredRegEx { get; set; }
        public string RegExRule { get; set; }

        public PropertySettingCacheItem()
        {

        }

        public PropertySettingCacheItem(string value, bool visible, bool requiredRegEx, string regExRule)
        {
            Value = value;
            Visible = visible;
            RequiredRegEx = requiredRegEx;
            RegExRule = regExRule;
        }

        public static string CalculateCacheKey(Guid? tenantId, string name, string providerName, string providerKey)
        {
            return string.Format(CacheKeyFormat, tenantId, providerName, providerKey, name);
        }

        public static string GetSettingNameFormCacheKeyOrNull(string cacheKey)
        {
            var result = FormattedStringValueExtracter.Extract(cacheKey, CacheKeyFormat, true);
            return result.IsMatch ? result.Matches.Last().Value : null;
        }
    }
}
