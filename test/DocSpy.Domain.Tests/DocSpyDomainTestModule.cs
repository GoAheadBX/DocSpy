using DocSpy.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace DocSpy
{
    [DependsOn(
        typeof(DocSpyEntityFrameworkCoreTestModule)
        )]
    public class DocSpyDomainTestModule : AbpModule
    {

    }
}