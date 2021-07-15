using DocSpy.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace DocSpy.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class DocSpyController : AbpController
    {
        protected DocSpyController()
        {
            LocalizationResource = typeof(DocSpyResource);
        }
    }
}