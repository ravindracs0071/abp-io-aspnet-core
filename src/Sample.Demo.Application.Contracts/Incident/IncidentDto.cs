using System;
using Volo.Abp.Application.Dtos;

namespace Sample.Demo.Incident
{
    public class IncidentDto : EntityDto<int>
    {
        public string IncidentNo { get; set; }
    }

    public class IncidentDetailDto : EntityDto<Guid>
    {
        public int IncidentMasterId { get; set; }

        public string IncidentNo { get; set; }

        public string IncidentDescr { get; set; }

        public int? IncidentType { get; set; }

        public DateTime? OccurenceDate { get; set; }

        public string ReportTo { get; set; }

        public string Status { get; set; }
    }

    public class ReviewDetailDto : EntityDto<Guid>
    {
        public Guid IncidentDetailId { get; set; }

        public bool IsIncidentValid { get; set; }

        public string Comments { get; set; }

        public string IncidentStatus { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
