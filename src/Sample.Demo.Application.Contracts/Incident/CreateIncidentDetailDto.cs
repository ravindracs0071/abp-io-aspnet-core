
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sample.Demo.Incident
{
    public class CreateIncidentDetailDto : IValidatableObject
    {
        [Required]
        public int IncidentMasterId { get; set; }

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

        public IEnumerable<ValidationResult> Validate(
            ValidationContext validationContext)
        {
            //Interface should be declared in Contracts
            //var incidentService = (Incident.IIncidentDetailAppService)validationContext.GetService(typeof(Incident.IIncidentDetailAppService));

            if (!IncidentType.HasValue)
            {
                yield return new ValidationResult(
                    "IncidentType can not be null!",
                    new[] { "IncidentType" }
                );
            }
        }
    }
}
