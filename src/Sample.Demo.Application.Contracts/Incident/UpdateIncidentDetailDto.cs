using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using Volo.Abp.Threading;

namespace Sample.Demo.Incident
{
    public class UpdateIncidentDetailDto : IValidatableObject
    {
        [Required]
        [StringLength(IncidentConsts.MaxIncidentNoLength)]
        public string IncidentNo { get; set; }

        [Required]
        [StringLength(IncidentConsts.MaxValueLength)]
        public string IncidentDescr { get; set; }

        public int? IncidentType { get; set; }

        [DataType(DataType.Date)]
        public DateTime? OccurenceDate { get; set; }

        public string ReportTo { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //Interface should be declared in Contracts
            PropertySetting.IEntitySettingsAppService _entitySettingsService = (PropertySetting.IEntitySettingsAppService)validationContext.GetService(typeof(PropertySetting.IEntitySettingsAppService));

            List<PropertySetting.PropertySettingValue> settingConfig = AsyncHelper.RunSync(() => _entitySettingsService.GetAsync());

            if (settingConfig != null && settingConfig.Count > 0)
            {
                var checkIncidentType = settingConfig.FirstOrDefault(s => s.Name == PropertySetting.PropertySettingNames.Incidents.IncidentType);

                if (checkIncidentType.Visible)
                {
                    if (IncidentType == null)
                    {
                        yield return new ValidationResult(string.Format("The {0} field is required.", "IncidentType"), new[] { "IncidentType" });
                    }
                }

                var checkOccurenceDate = settingConfig.FirstOrDefault(s => s.Name == PropertySetting.PropertySettingNames.Incidents.OccurenceDate);

                if (checkOccurenceDate.Visible)
                {
                    if (OccurenceDate == null)
                    {
                        yield return new ValidationResult(string.Format("The {0} field is required.", "OccurenceDate"), new[] { "OccurenceDate" });
                    }
                }

                var checkReportTo = settingConfig.FirstOrDefault(s => s.Name == PropertySetting.PropertySettingNames.Incidents.ReportTo);

                if (checkReportTo.Visible && checkReportTo.RequiredRegEx)
                {
                    if (ReportTo.IsNullOrEmpty())
                    {
                        yield return new ValidationResult(string.Format("The {0} field is required.", "ReportTo"), new[] { "ReportTo" });
                    }

                    if (!string.IsNullOrEmpty(checkReportTo.RegExRule) && new Regex(checkReportTo.RegExRule).IsMatch(ReportTo) == false)
                    {
                        yield return new ValidationResult(string.Format("The field {0} is invalid.", "ReportTo"), new[] { "ReportTo" });
                    }
                }
            }
        }
    }
}
