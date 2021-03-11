
using System;
using System.ComponentModel.DataAnnotations;

namespace Sample.Demo.Incident
{
    public class CreateIncidentDetailDto
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
    }
}
