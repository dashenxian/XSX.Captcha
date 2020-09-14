using XSX.Captcha.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace XSX.Captcha
{
    [DependsOn(
        typeof(CaptchaEntityFrameworkCoreTestModule)
        )]
    public class CaptchaDomainTestModule : AbpModule
    {

    }
}