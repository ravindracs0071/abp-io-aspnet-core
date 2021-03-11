using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Sample.Demo.Incident
{
    public interface IIncidentAppService : IApplicationService
    {
        Task<IncidentDto> GetAsync(int id);

        Task<PagedResultDto<IncidentDto>> GetListAsync(GetIncidentListDto input);

        Task<IncidentDto> CreateAsync(CreateIncidentDto input);

        Task DeleteAsync(int id);
    }
}
