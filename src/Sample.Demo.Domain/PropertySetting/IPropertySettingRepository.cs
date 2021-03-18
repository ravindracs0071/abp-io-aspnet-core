using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Sample.Demo.PropertySetting
{
    public interface IPropertySettingRepository : IBasicRepository<PropertySetting, Guid>
    {
        Task<PropertySetting> FindAsync(
            string name,
            string providerName,
            string providerKey,
            CancellationToken cancellationToken = default);

        Task<List<PropertySetting>> GetListAsync(
            string providerName,
            string providerKey,
            CancellationToken cancellationToken = default);

        Task<List<PropertySetting>> GetListAsync(
            string[] names,
            string providerName,
            string providerKey,
            CancellationToken cancellationToken = default);
    }
}
