
using Volo.Abp;

namespace Sample.Demo.Incident
{
    public class IncidentAlreadyExistsException : BusinessException
    {
        public IncidentAlreadyExistsException(string name)
            : base(DemoDomainErrorCodes.IncidentAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
