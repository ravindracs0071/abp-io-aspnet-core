using Localization.Resources.AbpUi;
using Microsoft.Extensions.Localization;
using Sample.Demo.Localization;
using System.Collections.Generic;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Abp.TenantManagement;

namespace Sample.Demo
{
    [DependsOn(
        typeof(DemoApplicationContractsModule),
        typeof(AbpAccountHttpApiModule),
        typeof(AbpIdentityHttpApiModule),
        typeof(AbpPermissionManagementHttpApiModule),
        typeof(AbpTenantManagementHttpApiModule),
        typeof(AbpFeatureManagementHttpApiModule)
        )]
    public class DemoHttpApiModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            ConfigureLocalization();
        }

        private void ConfigureLocalization()
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<DemoResource>()
                    .AddBaseTypes(
                        typeof(AbpUiResource)
                    );
                options.GlobalContributors.Add(typeof(DbLocalizationResourceContributor));
            });
        }
    }

    public class DbLocalizationResourceContributor : ILocalizationResourceContributor
    {
        private ILanguageTextAppService _languageTextAppService;
        public void Fill(string cultureName, Dictionary<string, LocalizedString> dictionary)
        {
            Dictionary<string, string> messages = _languageTextAppService.GetAllValuesFromDatabaseForCulture(cultureName);

            foreach (var message in messages)
            {
                dictionary[message.Key] = new LocalizedString(message.Key, message.Value);
            }
        }

        public LocalizedString GetOrNull(string cultureName, string name)
        {
            KeyValuePair<string, string> message = _languageTextAppService.Get(cultureName, name);

            if (message.Key == null)
            {
                return null;
            }

            return new LocalizedString(message.Key, message.Value);
        }

        public void Initialize(LocalizationResourceInitializationContext context)
        {
            _languageTextAppService = (ILanguageTextAppService)context.ServiceProvider.GetService(typeof(Sample.Demo.Localization.ILanguageTextAppService));
        }
    }

}
