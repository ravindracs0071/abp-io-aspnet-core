using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sample.Demo.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Sample.Demo.Incident
{
    [Authorize(DemoPermissions.Incidents.Default)]
    public class ReviewDetailAppService : DemoAppService, IReviewDetailAppService
    {
        private readonly IReviewDetailRepository _reviewDetailRepository;
        private readonly ReviewDetailManager _reviewDetailManager;

        public ReviewDetailAppService(
            IReviewDetailRepository reviewDetailRepository,
            ReviewDetailManager reviewDetailManager)
        {
            _reviewDetailRepository = reviewDetailRepository;
            _reviewDetailManager = reviewDetailManager;
        }

        public async Task<ReviewDetailDto> GetAsync(Guid id)
        {
            var reviewDetail = await _reviewDetailRepository.GetAsync(id);
            return ObjectMapper.Map<ReviewDetail, ReviewDetailDto>(reviewDetail);
        }

        public async Task<PagedResultDto<ReviewDetailDto>> GetListAsync(GetReviewDetailListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(ReviewDetail.CreationTime);
            }

            var reviewDetails = await _reviewDetailRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );

            var totalCount = input.Filter == null
                ? await _reviewDetailRepository.CountAsync()
                : await _reviewDetailRepository.CountAsync(review => review.IncidentStatus.Contains(input.Filter));

            return new PagedResultDto<ReviewDetailDto>(
                totalCount,
                ObjectMapper.Map<List<ReviewDetail>, List<ReviewDetailDto>>(reviewDetails)
            );
        }

        [Authorize(DemoPermissions.Incidents.Create)]
        public async Task<ReviewDetailDto> CreateAsync(CreateReviewDetailDto input)
        {
            var review = await _reviewDetailManager.CreateAsync(input.IncidentDetailId,
            input.IsIncidentValid,
            input.Comments,
            input.IncidentStatus,
            input.EndDate);

            await _reviewDetailRepository.InsertAsync(review);

            return ObjectMapper.Map<ReviewDetail, ReviewDetailDto>(review);
        }

        [Authorize(DemoPermissions.Incidents.Edit)]
        public async Task UpdateAsync(Guid id, UpdateReviewDetailDto input)
        {
            var review = await _reviewDetailRepository.GetAsync(id);

            //if (incident.IncidentNo != input.IncidentNo)
            //{
            //    await _reviewDetailManager.ChangeNameAsync(incident, input.IncidentNo);
            //}
            //TODO change methods
            await _reviewDetailRepository.UpdateAsync(review);
        }

        [Authorize(DemoPermissions.Incidents.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _reviewDetailRepository.DeleteAsync(id);
        }
    }
}
