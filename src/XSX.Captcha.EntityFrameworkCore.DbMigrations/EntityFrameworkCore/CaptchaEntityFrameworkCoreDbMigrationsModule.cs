using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace XSX.Captcha.EntityFrameworkCore
{
    [DependsOn(
        typeof(CaptchaEntityFrameworkCoreModule)
        )]
    public class CaptchaEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<CaptchaMigrationsDbContext>();
        }
    }
}
