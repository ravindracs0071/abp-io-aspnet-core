using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Sample.Demo.Incident
{
    public class ReviewDetailManager : DomainService
    {
        private readonly IReviewDetailRepository _reviewDetailRepository;
        private readonly IIncidentDetailRepository _incidentDetailRepository;
        public ReviewDetailManager(IReviewDetailRepository reviewDetailRepository, IIncidentDetailRepository incidentDetailRepository)
        {
            _reviewDetailRepository = reviewDetailRepository;
            _incidentDetailRepository = incidentDetailRepository;
        }

        public async Task<ReviewDetail> CreateAsync(
            [NotNull] Guid incidentDetailId,
            [NotNull] bool isIncidentValid,
            [NotNull] string comments,
            [CanBeNull] string incidentStatus = null,
            [CanBeNull] DateTime? endDate = null
            )
        {
            Check.NotNullOrWhiteSpace(comments, nameof(comments));

            //Existing Check
            var existingIncidentDetail = await _incidentDetailRepository.GetAsync(i => i.Id == incidentDetailId);
            if (existingIncidentDetail == null)
            {
                throw new ReviewDetailInvalidUniqueIdException(incidentDetailId);
            }

            return new ReviewDetail(
                GuidGenerator.Create(),
                incidentDetailId,
                isIncidentValid,
                comments,
                incidentStatus,
                endDate
            );
        }
    }
}