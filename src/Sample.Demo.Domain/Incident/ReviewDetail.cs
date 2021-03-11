using System;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Sample.Demo.Incident
{
    public class ReviewDetail : FullAuditedAggregateRoot<Guid>
    {
        //Foreign key
        public Guid IncidentDetailId { get; set; }
        public bool IsIncidentValid { get; set; }
        public string Comments { get; set; }
        public string IncidentStatus { get; set; }
        public DateTime? EndDate { get; set; }

        private ReviewDetail()
        {
            /* This constructor is for deserialization / ORM purpose */
        }

        internal ReviewDetail(
        Guid id,
        [NotNull] Guid incidentDetailId,
        [NotNull] bool isIncidentValid,
        [NotNull] string comments,
        [CanBeNull] string incidentStatus = null,
        [CanBeNull] DateTime? endDate = null
        )
        : base(id)
        {
            SetComments(comments);
            IncidentDetailId = incidentDetailId;
            IsIncidentValid = isIncidentValid;
            IncidentStatus = incidentStatus;
            EndDate = endDate;
        }

        internal ReviewDetail ChangeComments([NotNull] string comments)
        {
            SetComments(comments);
            return this;
        }

        private void SetComments([NotNull] string comments)
        {
            Comments = Check.NotNullOrWhiteSpace(
                comments,
                nameof(comments),
                maxLength: IncidentConsts.MaxValueLength
            );
        }
    }
}
