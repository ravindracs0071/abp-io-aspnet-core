using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sample.Demo.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using System.Linq;

namespace Sample.Demo.Incident
{
    [Authorize(DemoPermissions.Incidents.Default)]
    public class IncidentDetailAppService : DemoAppService, IIncidentDetailAppService
    {
        private readonly IIncidentDetailRepository _incidentDetailRepository;
        private readonly IncidentDetailManager _incidentDetailManager;

        private readonly IIncidentRepository _incidentRepository;
        private readonly IReviewDetailRepository _reviewDetailRepository;

        public IncidentDetailAppService(
            IIncidentDetailRepository incidentDetailRepository,
            IncidentDetailManager incidentDetailManager,
            IIncidentRepository incidentRepository,
            IReviewDetailRepository reviewDetailRepository)
        {
            _incidentDetailRepository = incidentDetailRepository;
            _incidentDetailManager = incidentDetailManager;
            _incidentRepository = incidentRepository;
            _reviewDetailRepository = reviewDetailRepository;
         }

        public async Task<IncidentDetailDto> GetAsync(Guid id)
        {
            var incident = await _incidentDetailRepository.GetAsync(id);
            var incidentMaster = await _incidentRepository.GetAsync(i => i.Id == incident.IncidentMasterId);
            var incidentDetailDto = ObjectMapper.Map<IncidentDetail, IncidentDetailDto>(incident);
            incidentDetailDto.IncidentNo = incidentMaster.IncidentNo;
            return incidentDetailDto;
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

            IQueryable<IncidentMaster> incidentMasters = await _incidentRepository.GetQueryableAsync();

            IQueryable<ReviewDetail> reviewDetails = await _reviewDetailRepository.GetQueryableAsync();

            var incidentDetailDtos = from incidentDetail in incidents
                    join incidentMaster in incidentMasters.ToList() on incidentDetail.IncidentMasterId equals incidentMaster.Id
                    join reviewDetail in reviewDetails.ToList() on incidentDetail.Id equals reviewDetail.IncidentDetailId into reviewDetailTemp
                    from reviewDetail in reviewDetailTemp.DefaultIfEmpty()
                    select new IncidentDetailDto
                    {
                        Id = incidentDetail.Id,
                        IncidentMasterId = incidentMaster.Id,
                        IncidentNo = incidentMaster.IncidentNo,
                        IncidentDescr = incidentDetail.IncidentDescr,
                        IncidentType = incidentDetail.IncidentType,
                        OccurenceDate = incidentDetail.OccurenceDate,
                        ReportTo = incidentDetail.ReportTo,
                        Status = ((reviewDetailTemp == null || reviewDetailTemp.Count() == 0) ? "Pending Assign" : reviewDetailTemp.LastOrDefault().IncidentStatus)
                    };
            //ObjectMapper.Map<List<IncidentDetail>, List<IncidentDetailDto>>(incidents) - return
            return new PagedResultDto<IncidentDetailDto>(
                totalCount,
                incidentDetailDtos.ToList()
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

            if (incident.IncidentDescr != input.IncidentDescr)
            {
                incident.IncidentDescr = input.IncidentDescr;
            }

            if (incident.IncidentType != input.IncidentType)
            {
                incident.IncidentType = input.IncidentType;
            }

            if (incident.OccurenceDate != input.OccurenceDate)
            {
                incident.OccurenceDate = input.OccurenceDate;
            }

            if (incident.ReportTo != input.ReportTo)
            {
                incident.ReportTo = input.ReportTo;
            }

            await _incidentDetailRepository.UpdateAsync(incident);
        }

        [Authorize(DemoPermissions.Incidents.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _incidentDetailRepository.DeleteAsync(id);
        }
    }
}
