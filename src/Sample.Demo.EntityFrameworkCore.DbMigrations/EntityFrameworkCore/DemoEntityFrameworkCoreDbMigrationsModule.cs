using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Sample.Demo.EntityFrameworkCore
{
    [DependsOn(
        typeof(DemoEntityFrameworkCoreModule)
        )]
    public class DemoEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<DemoMigrationsDbContext>();
        }
    }
}
