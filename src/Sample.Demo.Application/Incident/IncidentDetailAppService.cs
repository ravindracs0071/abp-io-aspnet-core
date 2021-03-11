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
    public class IncidentDetailAppService : DemoAppService, IIncidentDetailAppService
    {
        private readonly IIncidentDetailRepository _incidentDetailRepository;
        private readonly IncidentDetailManager _incidentDetailManager;

        public IncidentDetailAppService(
            IIncidentDetailRepository incidentDetailRepository,
            IncidentDetailManager incidentDetailManager)
        {
            _incidentDetailRepository = incidentDetailRepository;
            _incidentDetailManager = incidentDetailManager;
         }

        public async Task<IncidentDetailDto> GetAsync(Guid id)
        {
            var incident = await _incidentDetailRepository.GetAsync(id);
            return ObjectMapper.Map<IncidentDetail, IncidentDetailDto>(incident);
        }

        public async Task<PagedResultDto<IncidentDetailDto>> GetListAsync(GetIncidentDetailListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(IncidentDetail.CreationTime);
            }

            var incidents = await _incidentDetailRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );

            var totalCount = input.Filter == null
                ? await _incidentDetailRepository.CountAsync()
                : await _incidentDetailRepository.CountAsync(incident => incident.IncidentDescr.Contains(input.Filter));

            return new PagedResultDto<IncidentDetailDto>(
                totalCount,
                ObjectMapper.Map<List<IncidentDetail>, List<IncidentDetailDto>>(incidents)
            );
        }

        [Authorize(DemoPermissions.Incidents.Create)]
        public async Task<IncidentDetailDto> CreateAsync(CreateIncidentDetailDto input)
        {
            var incident = await _incidentDetailManager.CreateAsync(input.IncidentMasterId, input.IncidentDescr, input.IncidentType, input.OccurenceDate, input.ReportTo);

            await _incidentDetailRepository.InsertAsync(incident);

            return ObjectMapper.Map<IncidentDetail, IncidentDetailDto>(incident);
        }

        [Authorize(DemoPermissions.Incidents.Edit)]
        public async Task UpdateAsync(Guid id, UpdateIncidentDetailDto input)
        {
            var incident = await _incidentDetailRepository.GetAsync(id);

            //if (incident.IncidentDescr != input.IncidentDescr)
            //{
            //    await _incidentDetailManager.ChangeNameAsync(incident, input.IncidentNo);
            //}
            //TODO method
            await _incidentDetailRepository.UpdateAsync(incident);
        }

        [Authorize(DemoPermissions.Incidents.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _incidentDetailRepository.DeleteAsync(id);
        }
    }
}
