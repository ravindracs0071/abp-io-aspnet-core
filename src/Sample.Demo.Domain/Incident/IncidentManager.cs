using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Sample.Demo.Incident
{
    public class IncidentManager : DomainService
    {
        private readonly IIncidentRepository _incidentRepository;

        public IncidentManager(IIncidentRepository incidentRepository)
        {
            _incidentRepository = incidentRepository;
        }

        public async Task<IncidentMaster> CreateAsync(
            [NotNull] string incidentNo)
        {
            Check.NotNullOrWhiteSpace(incidentNo, nameof(incidentNo));

            var existingIncident = await _incidentRepository.FindByNameAsync(incidentNo);
            if (existingIncident != null)
            {
                throw new IncidentAlreadyExistsException(incidentNo);
            }

            return new IncidentMaster(
                IncidentConsts.DefaultInsertId,
                incidentNo
            );
        }

        public async Task ChangeNameAsync(
            [NotNull] IncidentMaster incidentMaster,
            [NotNull] string newIncidentNo)
        {
            Check.NotNull(incidentMaster, nameof(incidentMaster));
            Check.NotNullOrWhiteSpace(newIncidentNo, nameof(newIncidentNo));

            var existingIncident = await _incidentRepository.FindByNameAsync(newIncidentNo);
            if (existingIncident != null && existingIncident.Id != incidentMaster.Id)
            {
                throw new IncidentAlreadyExistsException(newIncidentNo);
            }

            incidentMaster.ChangeIncidentNo(newIncidentNo);
        }
    }
}
