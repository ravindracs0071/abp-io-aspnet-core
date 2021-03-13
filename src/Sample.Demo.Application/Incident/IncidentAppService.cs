using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sample.Demo.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

namespace Sample.Demo.Incident
{
    [Authorize(DemoPermissions.Incidents.Default)]
    public class IncidentAppService : DemoAppService, IIncidentAppService
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly IncidentManager _incidentManager;

        public IncidentAppService(
            IIncidentRepository incidentRepository,
            IncidentManager incidentManager)
        {
            _incidentRepository = incidentRepository;
            _incidentManager = incidentManager;
        }

        public async Task<IncidentDto> GetAsync(int id)
        {
            var incident = await _incidentRepository.GetAsync(id);
            return ObjectMapper.Map<IncidentMaster, IncidentDto>(incident);
        }

        public async Task<PagedResultDto<IncidentDto>> GetListAsync(GetIncidentListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(IncidentMaster.CreationTime);
            }

            var incidents = await _incidentRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );


            var totalCount = input.Filter == null
                ? await _incidentRepository.CountAsync()
                : await _incidentRepository.CountAsync(incident => incident.IncidentNo.Contains(input.Filter));

            return new PagedResultDto<IncidentDto>(
                totalCount,
                ObjectMapper.Map<List<IncidentMaster>, List<IncidentDto>>(incidents)
            );
        }

        [Authorize(DemoPermissions.Incidents.Create)]
        [UnitOfWork]
        public async Task<IncidentDto> CreateAsync()
        {
            var incident = await _incidentManager.CreateAsync(String.Format("INC-TEMP-{0}", DateTime.UtcNow.ToString("ddMMyyyyHHmmssfff")));

            await _incidentRepository.InsertAsync(incident, autoSave: true);

            var incidentMaster = await _incidentRepository.GetAsync(incident.Id);

            string incidentNo = String.Format("INC-{0:0000}", incident.Id);

            await _incidentManager.ChangeNameAsync(incidentMaster, incidentNo);

            await _incidentRepository.UpdateAsync(incident);

            return ObjectMapper.Map<IncidentMaster, IncidentDto>(incident);
        }

        [Authorize(DemoPermissions.Incidents.Edit)]
        public async Task<IncidentDto> UpdateAsync(int id, UpdateIncidentDto input)
        {
            var incident = await _incidentRepository.GetAsync(id);

            input.IncidentNo = String.Format("INC-{0:0000}", id);
            
            if (incident.IncidentNo != input.IncidentNo)
            {
                await _incidentManager.ChangeNameAsync(incident, input.IncidentNo);
            }

            await _incidentRepository.UpdateAsync(incident);

            return ObjectMapper.Map<IncidentMaster, IncidentDto>(incident);
        }

        [Authorize(DemoPermissions.Incidents.Delete)]
        public async Task DeleteAsync(int id)
        {
            await _incidentRepository.DeleteAsync(id);
        }
    }
}
