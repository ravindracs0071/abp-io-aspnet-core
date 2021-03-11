using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Sample.Demo.Incident
{
    public interface IIncidentDetailRepository : IRepository<IncidentDetail, Guid>
    {
        Task<IncidentDetail> FindByIncidentMasterIdAsync(int incidentMasterId);

        Task<List<IncidentDetail>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );
    }
}
