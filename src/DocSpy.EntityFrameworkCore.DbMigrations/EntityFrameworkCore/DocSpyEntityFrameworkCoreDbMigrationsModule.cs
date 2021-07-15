using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace DocSpy.EntityFrameworkCore
{
    [DependsOn(
        typeof(DocSpyEntityFrameworkCoreModule)
        )]
    public class DocSpyEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<DocSpyMigrationsDbContext>();
        }
    }
}
