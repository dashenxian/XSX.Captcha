using System;
using System.Collections.Generic;
using System.Text;
using XSX.Captcha.Localization;
using Volo.Abp.Application.Services;

namespace XSX.Captcha
{
    /* Inherit your application services from this class.
     */
    public abstract class CaptchaAppService : ApplicationService
    {
        protected CaptchaAppService()
        {
            LocalizationResource = typeof(CaptchaResource);
        }
    }
}
