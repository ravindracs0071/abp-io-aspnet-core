using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Sample.Demo.Incident
{
    public interface IReviewDetailAppService : IApplicationService
    {
        Task<ReviewDetailDto> GetAsync(Guid id);

        Task<PagedResultDto<ReviewDetailDto>> GetListAsync(GetReviewDetailListDto input);

        Task<ReviewDetailDto> CreateAsync(CreateReviewDetailDto input);

        Task UpdateAsync(Guid id, UpdateReviewDetailDto input);

        Task DeleteAsync(Guid id);
    }
}
