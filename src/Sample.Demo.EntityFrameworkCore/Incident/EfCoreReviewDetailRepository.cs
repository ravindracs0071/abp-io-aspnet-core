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
    public class EfCoreReviewDetailRepository
    : EfCoreRepository<DemoDbContext, ReviewDetail, Guid>,
    IReviewDetailRepository
    {
        public EfCoreReviewDetailRepository(
               IDbContextProvider<DemoDbContext> dbContextProvider)
               : base(dbContextProvider)
        {
        }

        public async Task<ReviewDetail> FindByIncidentDetailIdAsync(Guid incidentDetailId)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(review => review.IncidentDetailId == incidentDetailId);
        }

        public async Task<List<ReviewDetail>> GetListAsync(
           int skipCount,
           int maxResultCount,
           string sorting,
           string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    review => review.IncidentStatus.Contains(filter)
                )
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
