using Volo.Abp;

namespace Sample.Demo.Incident
{
    public class ReviewDetailInvalidUniqueIdException : BusinessException
    {
        public ReviewDetailInvalidUniqueIdException(System.Guid incidentDetailId)
            : base(DemoDomainErrorCodes.InvalidUniqueId)
        {
            WithData("incidentDetailId", incidentDetailId);
        }
    }
}
