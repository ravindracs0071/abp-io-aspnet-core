using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Sample.Demo.Incident
{
    public interface IIncidentDetailAppService : IApplicationService
    {
        Task<IncidentDetailDto> GetAsync(Guid id);

        Task<PagedResultDto<IncidentDetailDto>> GetListAsync(GetIncidentDetailListDto input);

        Task<IncidentDetailDto> CreateAsync(CreateIncidentDetailDto input);

        Task UpdateAsync(Guid id, UpdateIncidentDetailDto input);

        Task DeleteAsync(Guid id);
    }
}
