using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Sample.Demo.Incident
{
    public interface IReviewDetailRepository : IRepository<ReviewDetail, Guid>
    {
        Task<List<ReviewDetail>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );
    }
}
