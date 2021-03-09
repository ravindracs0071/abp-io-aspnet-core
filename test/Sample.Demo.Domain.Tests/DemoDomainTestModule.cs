using Sample.Demo.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Sample.Demo
{
    [DependsOn(
        typeof(DemoEntityFrameworkCoreTestModule)
        )]
    public class DemoDomainTestModule : AbpModule
    {

    }
}