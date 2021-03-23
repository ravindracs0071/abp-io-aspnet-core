using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Sample.Demo.PropertySetting
{
    public interface IEntitySettingsAppService : IApplicationService
    {
        Task<Dictionary<string, PropertySettingValue>> GetAsync();
    }
}
