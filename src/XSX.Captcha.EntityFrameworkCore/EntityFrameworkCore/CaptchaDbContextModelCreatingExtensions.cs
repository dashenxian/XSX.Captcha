using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace XSX.Captcha.EntityFrameworkCore
{
    public static class CaptchaDbContextModelCreatingExtensions
    {
        public static void ConfigureCaptcha(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(CaptchaConsts.DbTablePrefix + "YourEntities", CaptchaConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});
        }
    }
}