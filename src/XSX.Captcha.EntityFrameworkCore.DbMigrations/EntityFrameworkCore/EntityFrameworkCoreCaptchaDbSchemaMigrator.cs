using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using XSX.Captcha.Data;
using Volo.Abp.DependencyInjection;

namespace XSX.Captcha.EntityFrameworkCore
{
    public class EntityFrameworkCoreCaptchaDbSchemaMigrator
        : ICaptchaDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreCaptchaDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the CaptchaMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<CaptchaMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}