using XSX.Captcha.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace XSX.Captcha.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(CaptchaEntityFrameworkCoreDbMigrationsModule),
        typeof(CaptchaApplicationContractsModule)
        )]
    public class CaptchaDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
