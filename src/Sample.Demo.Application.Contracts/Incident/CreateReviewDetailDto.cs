using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using Volo.Abp.Threading;

namespace Sample.Demo.Incident
{
    public class CreateReviewDetailDto : IValidatableObject
    {
        [Required]
        public Guid IncidentDetailId { get; set; }

        [Required]
        public bool IsIncidentValid { get; set; }

        [Required]
        [StringLength(IncidentConsts.MaxValueLength)]
        public string Comments { get; set; }

        public string IncidentStatus { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //Interface should be declared in Contracts
            PropertySetting.IEntitySettingsAppService _entitySettingsService = (PropertySetting.IEntitySettingsAppService)validationContext.GetService(typeof(PropertySetting.IEntitySettingsAppService));

            Dictionary<string, PropertySetting.PropertySettingValue> settingConfig = AsyncHelper.RunSync(() => _entitySettingsService.GetAsync());

            if (settingConfig != null && settingConfig.Count > 0)
            {
                PropertySetting.PropertySettingValue checkIncidentStatus = settingConfig[PropertySetting.PropertySettingNames.Incidents.Reviews.IncidentStatus];

                if (checkIncidentStatus.Visible)
                {
                    if (IncidentStatus.IsNullOrEmpty())
                    {
                        yield return new ValidationResult(string.Format("The {0} field is required.", "IncidentStatus"), new[] { "IncidentStatus" });
                    }

                    if (checkIncidentStatus.RequiredRegEx)
                    {
                        if (!string.IsNullOrEmpty(checkIncidentStatus.RegExRule) && new Regex(checkIncidentStatus.RegExRule).IsMatch(IncidentStatus) == false)
                        {
                            yield return new ValidationResult(string.Format("The field {0} is invalid.", "IncidentStatus"), new[] { "IncidentStatus" });
                        }
                    }
                }

                PropertySetting.PropertySettingValue checkEndDate = settingConfig[PropertySetting.PropertySettingNames.Incidents.Reviews.EndDate];

                if (checkEndDate.Visible)
                {
                    if (EndDate == null)
                    {
                        yield return new ValidationResult(string.Format("The {0} field is required.", "EndDate"), new[] { "EndDate" });
                    }
                }
            }
        }
    }
}
