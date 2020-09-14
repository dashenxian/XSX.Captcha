using XSX.Captcha.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace XSX.Captcha.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class CaptchaController : AbpController
    {
        protected CaptchaController()
        {
            LocalizationResource = typeof(CaptchaResource);
        }
    }
}