using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Sample.Demo.Incident
{
    public interface IIncidentRepository : IRepository<IncidentMaster, int>
    {
        Task<IncidentMaster> FindByNameAsync(string incidentNo);

        Task<List<IncidentMaster>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );
    }
}
