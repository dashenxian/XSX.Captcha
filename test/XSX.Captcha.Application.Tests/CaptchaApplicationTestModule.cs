using Volo.Abp.Modularity;

namespace XSX.Captcha
{
    [DependsOn(
        typeof(CaptchaApplicationModule),
        typeof(CaptchaDomainTestModule)
        )]
    public class CaptchaApplicationTestModule : AbpModule
    {

    }
}