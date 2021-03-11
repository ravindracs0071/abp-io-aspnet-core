using System;
using System.ComponentModel.DataAnnotations;

namespace Sample.Demo.Incident
{
    public class CreateReviewDetailDto
    {
        [Required]
        public Guid IncidentDetailId { get; set; }

        [Required]
        public bool IsIncidentValid { get; set; }

        [Required]
        [StringLength(IncidentConsts.MaxValueLength)]
        public string Comments { get; set; }

        [StringLength(IncidentConsts.MaxNameLength)]
        public string IncidentStatus { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
    }
}
