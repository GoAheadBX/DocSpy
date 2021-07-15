using System;
using System.Collections.Generic;
using System.Text;
using DocSpy.Localization;
using Volo.Abp.Application.Services;

namespace DocSpy
{
    /* Inherit your application services from this class.
     */
    public abstract class DocSpyAppService : ApplicationService
    {
        protected DocSpyAppService()
        {
            LocalizationResource = typeof(DocSpyResource);
        }
    }
}
