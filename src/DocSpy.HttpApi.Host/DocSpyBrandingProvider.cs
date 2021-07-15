using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace DocSpy
{
    [Dependency(ReplaceServices = true)]
    public class DocSpyBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "DocSpy";
    }
}
