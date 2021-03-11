using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Sample.Demo.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Sample.Demo.Incident
{
    public class EfCoreIncidentRepository
       : EfCoreRepository<DemoDbContext, IncidentMaster, int>,
           IIncidentRepository
    {
        public EfCoreIncidentRepository(
               IDbContextProvider<DemoDbContext> dbContextProvider)
               : base(dbContextProvider)
        {
        }

        public async Task<IncidentMaster> FindByNameAsync(string incidentNo)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(incident => incident.IncidentNo == incidentNo);
        }

        public async Task<List<IncidentMaster>> GetListAsync(
           int skipCount,
           int maxResultCount,
           string sorting,
           string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    incident => incident.IncidentNo.Contains(filter)
                )
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
