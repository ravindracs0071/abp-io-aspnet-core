using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Demo.PropertySetting
{
    public class EntitySettingsAppService : DemoAppService, IEntitySettingsAppService
    {
        public const string ProviderName = "T";
        protected IPropertySettingManagementStore SettingStore { get; }
        public EntitySettingsAppService(IPropertySettingManagementStore settingStore)
        {
            SettingStore = settingStore;
        }

        public virtual async Task<Dictionary<string, PropertySettingValue>> GetAsync() 
        {
            var propSettings = await SettingStore.GetListAsync(new string[] 
            {
                PropertySettingNames.Incidents.IncidentNo,
                PropertySettingNames.Incidents.IncidentDescription,
                PropertySettingNames.Incidents.IncidentType,
                PropertySettingNames.Incidents.OccurenceDate,
                PropertySettingNames.Incidents.ReportTo,
                PropertySettingNames.Incidents.Status,
                PropertySettingNames.Incidents.Reviews.IsIncidentValid,
                PropertySettingNames.Incidents.Reviews.Comments,
                PropertySettingNames.Incidents.Reviews.IncidentStatus,
                PropertySettingNames.Incidents.Reviews.EndDate
            },
            ProviderName, null);
            return propSettings;
        }
    }
}
