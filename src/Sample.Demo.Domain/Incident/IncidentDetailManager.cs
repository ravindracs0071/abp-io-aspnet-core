using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Sample.Demo.Incident
{
    public class IncidentDetailManager : DomainService
    {
        private readonly IIncidentDetailRepository _incidentDetailRepository;

        public IncidentDetailManager(IIncidentDetailRepository incidentDetailRepository)
        {
            _incidentDetailRepository = incidentDetailRepository;
        }

        public async Task<IncidentDetail> CreateAsync(
            [NotNull] int incidentMasterId,
            [NotNull] string incidentDescr,
            [CanBeNull] int? incidentType = null,
            [CanBeNull] DateTime? occurenceDate = null,
            [CanBeNull] string reportTo = null)
        {
            Check.NotNullOrWhiteSpace(incidentDescr, nameof(incidentDescr));

            var existingIncidentDetail = await _incidentDetailRepository.FindByIncidentMasterIdAsync(incidentMasterId);
            if (existingIncidentDetail != null)
            {
                throw new IncidentDetailAlreadyExistsException(incidentMasterId);
            }

            return new IncidentDetail(
                GuidGenerator.Create(),
                incidentMasterId,
                incidentDescr,
                incidentType,
                occurenceDate,
                reportTo
            );
        }
    }
}
