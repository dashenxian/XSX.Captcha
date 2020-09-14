using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace XSX.Captcha.Data
{
    /* This is used if database provider does't define
     * ICaptchaDbSchemaMigrator implementation.
     */
    public class NullCaptchaDbSchemaMigrator : ICaptchaDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}