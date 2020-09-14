using XSX.Captcha.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace XSX.Captcha.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class CaptchaPageModel : AbpPageModel
    {
        protected CaptchaPageModel()
        {
            LocalizationResourceType = typeof(CaptchaResource);
        }
    }
}