using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace XSX.Captcha.HttpApi.Client.ConsoleTestApp
{
    [DependsOn(
        typeof(CaptchaHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class CaptchaConsoleApiClientModule : AbpModule
    {
        
    }
}
