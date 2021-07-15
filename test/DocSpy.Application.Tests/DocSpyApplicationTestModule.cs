using Volo.Abp.Modularity;

namespace DocSpy
{
    [DependsOn(
        typeof(DocSpyApplicationModule),
        typeof(DocSpyDomainTestModule)
        )]
    public class DocSpyApplicationTestModule : AbpModule
    {

    }
}