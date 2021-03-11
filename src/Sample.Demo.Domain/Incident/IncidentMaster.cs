using System;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Sample.Demo.Incident
{
    public class IncidentMaster : FullAuditedAggregateRoot<int>
    {
        public string IncidentNo { get; private set; }

        private IncidentMaster()
        {
            /* This constructor is for deserialization / ORM purpose */
        }

        internal IncidentMaster(
        int id,
        [NotNull] string incidentNo)
        : base(id)
        {
            SetIncidentNo(incidentNo);
        }

        internal IncidentMaster ChangeIncidentNo([NotNull] string incidentNo)
        {
            SetIncidentNo(incidentNo);
            return this;
        }

        private void SetIncidentNo([NotNull] string incidentNo)
        {
            IncidentNo = Check.NotNullOrWhiteSpace(
                incidentNo,
                nameof(incidentNo),
                maxLength: IncidentConsts.MaxIncidentNoLength
            );
        }
    }
}
