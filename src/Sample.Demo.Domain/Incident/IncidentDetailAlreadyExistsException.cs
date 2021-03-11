using Volo.Abp;

namespace Sample.Demo.Incident
{
    public class IncidentDetailAlreadyExistsException : BusinessException
    {
        public IncidentDetailAlreadyExistsException(int incincidentMasterId)
            : base(DemoDomainErrorCodes.IncidentAlreadyExists)
        {
            WithData("incidentMasterId", incincidentMasterId);
        }
    }
}
