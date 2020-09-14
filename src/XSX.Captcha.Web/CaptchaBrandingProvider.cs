using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Components;
using Volo.Abp.DependencyInjection;

namespace XSX.Captcha.Web
{
    [Dependency(ReplaceServices = true)]
    public class CaptchaBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "Captcha";
    }
}
