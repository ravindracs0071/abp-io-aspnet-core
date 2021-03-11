using System;
using System.ComponentModel.DataAnnotations;

namespace Sample.Demo.Incident
{
    public class CreateIncidentDto
    {
        [Required]
        [StringLength(IncidentConsts.MaxIncidentNoLength)]
        public string IncidentNo { get; set; }
    }
}
