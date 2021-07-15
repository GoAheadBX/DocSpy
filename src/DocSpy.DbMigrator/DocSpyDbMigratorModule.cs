using DocSpy.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace DocSpy.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(DocSpyEntityFrameworkCoreDbMigrationsModule),
        typeof(DocSpyApplicationContractsModule)
        )]
    public class DocSpyDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
