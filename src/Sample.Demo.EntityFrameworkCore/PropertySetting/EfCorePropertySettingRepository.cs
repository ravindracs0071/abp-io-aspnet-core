using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Sample.Demo.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Sample.Demo.PropertySetting
{

    public class EfCorePropertySettingRepository : EfCoreRepository<DemoDbContext, PropertySetting, Guid>,
    IPropertySettingRepository
    {
        public EfCorePropertySettingRepository(IDbContextProvider<DemoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public virtual async Task<PropertySetting> FindAsync(
            string name,
            string providerName,
            string providerKey,
            CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .OrderBy(x => x.Id)
                .FirstOrDefaultAsync(
                    s => s.Name == name && s.ProviderName == providerName && s.ProviderKey == providerKey,
                    GetCancellationToken(cancellationToken));
        }

        public virtual async Task<List<PropertySetting>> GetListAsync(
            string providerName,
            string providerKey,
            CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .Where(
                    s => s.ProviderName == providerName && s.ProviderKey == providerKey
                ).ToListAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<List<PropertySetting>> GetListAsync(
            string[] names,
            string providerName,
            string providerKey,
            CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .Where(
                    s => names.Contains(s.Name) && s.ProviderName == providerName && s.ProviderKey == providerKey
                ).ToListAsync(GetCancellationToken(cancellationToken));
        }
    }
}
