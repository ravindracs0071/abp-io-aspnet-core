using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sample.Demo.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;
using Volo.Abp.Reflection;
using System.Reflection;
using Volo.Abp.Users;

namespace Sample.Demo.Incident
{
    public class IncidentPropertiesAppService : DemoAppService
    {
        private readonly ICurrentUser _currentUser;
        protected PropertySetting.IPropertySettingManager PropertySettingManager { get; }
        public IncidentPropertiesAppService(ICurrentUser currentUser, PropertySetting.IPropertySettingManager propertySettingManager)
        {
            _currentUser = currentUser;
            PropertySettingManager = propertySettingManager;
        }

        public virtual async Task<dynamic> GetAsync()
        {

            return new 
            {
                IncidentNo = await PropertySettingManager.GetOrNullAsync(PropertySettingNames.Incidents.IncidentNo, Volo.Abp.Settings.GlobalSettingValueProvider.ProviderName, null),
                //SmtpPort = Convert.ToInt32(await SettingManager.GetOrNullGlobalAsync(EmailSettingNames.Smtp.Port)),
                //SmtpUserName = await SettingManager.GetOrNullGlobalAsync(EmailSettingNames.Smtp.UserName),
                //SmtpPassword = await SettingManager.GetOrNullGlobalAsync(EmailSettingNames.Smtp.Password),
                //SmtpDomain = await SettingManager.GetOrNullGlobalAsync(EmailSettingNames.Smtp.Domain),
                //SmtpEnableSsl = Convert.ToBoolean(await SettingManager.GetOrNullGlobalAsync(EmailSettingNames.Smtp.EnableSsl)),
                //SmtpUseDefaultCredentials = Convert.ToBoolean(await SettingManager.GetOrNullGlobalAsync(EmailSettingNames.Smtp.UseDefaultCredentials)),
                //DefaultFromAddress = await SettingManager.GetOrNullGlobalAsync(EmailSettingNames.DefaultFromAddress),
                //DefaultFromDisplayName = await SettingManager.GetOrNullGlobalAsync(EmailSettingNames.DefaultFromDisplayName),
            };

            //Dictionary<string, string> config = new Dictionary<string, string>();
            //PropertyInfo[] propertyInfos;
            //propertyInfos = typeof(CreateIncidentDetailDto).GetProperties();
            //foreach (var property in propertyInfos)
            //{
            //    config.Add(property.Name, "");
            //}
            //return config;
        }
    }

    /// <summary>
    /// Declares names of the property settings.
    /// </summary>
    public static class PropertySettingNames
    {
        ///// <summary>
        ///// Abp.<......>
        ///// </summary>
        //public const string DefaultFromAddress = "Abp.<....>";

        /// <summary>
        /// Incidents settings.
        /// </summary>
        public static class Incidents
        {
            /// <summary>
            /// Abp.Incidents.IncidentNo
            /// </summary>
            public const string IncidentNo = "Abp.Incidents.IncidentNo";

            /// <summary>
            /// Abp.Incidents.IncidentDescr
            /// </summary>
            public const string IncidentDescription = "Abp.Incidents.IncidentDescr";

            /// <summary>
            /// Abp.Incidents.IncidentType
            /// </summary>
            public const string IncidentType = "Abp.Incidents.IncidentType";

            /// <summary>
            /// Abp.Incidents.OccurenceDate
            /// </summary>
            public const string OccurenceDate = "Abp.Incidents.OccurenceDate";

            /// <summary>
            /// Abp.Incidents.ReportTo
            /// </summary>
            public const string ReportTo = "Abp.Incidents.ReportTo";

            /// <summary>
            /// Abp.Incidents.Status
            /// </summary>
            public const string Status = "Abp.Incidents.Status";

            /// <summary>
            /// Reviews settings.
            /// </summary>
            public static class Reviews
            {
                /// <summary>
                /// Abp.Incidents.Status
                /// </summary>
                public const string IncidentStatus = "Abp.Incidents.Reviews.IncidentStatus";
            }
        }
    }
}
