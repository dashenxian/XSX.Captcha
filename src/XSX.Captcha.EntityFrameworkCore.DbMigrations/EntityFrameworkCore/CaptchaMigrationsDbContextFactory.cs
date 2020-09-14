using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace XSX.Captcha.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class CaptchaMigrationsDbContextFactory : IDesignTimeDbContextFactory<CaptchaMigrationsDbContext>
    {
        public CaptchaMigrationsDbContext CreateDbContext(string[] args)
        {
            CaptchaEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<CaptchaMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"));

            return new CaptchaMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../XSX.Captcha.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
