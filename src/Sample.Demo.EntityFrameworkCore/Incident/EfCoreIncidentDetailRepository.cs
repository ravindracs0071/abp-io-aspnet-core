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
    public class EfCoreIncidentDetailRepository
   : EfCoreRepository<DemoDbContext, IncidentDetail, Guid>,
       IIncidentDetailRepository
    {
        public EfCoreIncidentDetailRepository(
               IDbContextProvider<DemoDbContext> dbContextProvider)
               : base(dbContextProvider)
        {
        }

        public async Task<IncidentDetail> FindByIncidentMasterIdAsync(int incidentMasterId)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(incident => incident.IncidentMasterId == incidentMasterId);
        }

        public async Task<List<IncidentDetail>> GetListAsync(
           int skipCount,
           int maxResultCount,
           string sorting,
           string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    incident => incident.IncidentDescr.Contains(filter)
                )
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
