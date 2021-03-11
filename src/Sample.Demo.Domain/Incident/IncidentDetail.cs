using System;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Sample.Demo.Incident
{
    public class IncidentDetail : FullAuditedAggregateRoot<Guid>
    {
        //Foreign key
        public int IncidentMasterId { get; set; }

        public string IncidentDescr { get; set; }

        public int? IncidentType { get; set; }

        public DateTime? OccurenceDate { get; set; }

        public string ReportTo { get; set; }

        private IncidentDetail()
        {
            /* This constructor is for deserialization / ORM purpose */
        }

        internal IncidentDetail(
        Guid id,
        [NotNull] int incidentMasterId,
        [NotNull] string incidentDescr,
        [CanBeNull] int? incidentType = null,
        [CanBeNull] DateTime? occurenceDate = null,
        [CanBeNull] string reportTo = null)
        : base(id)
        {
            SetIncidentDescr(incidentDescr);
            IncidentMasterId = incidentMasterId;
            IncidentType = incidentType;
            OccurenceDate = occurenceDate;
            ReportTo = reportTo;
        }

        internal IncidentDetail ChangeIncidentDescr([NotNull] string incidentDescr)
        {
            SetIncidentDescr(incidentDescr);
            return this;
        }

        private void SetIncidentDescr([NotNull] string incidentDescr)
        {
            IncidentDescr = Check.NotNullOrWhiteSpace(
                incidentDescr,
                nameof(incidentDescr),
                maxLength: IncidentConsts.MaxValueLength
            );
        }
    }
}
