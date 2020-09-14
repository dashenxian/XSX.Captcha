using System.Threading.Tasks;

namespace XSX.Captcha.Data
{
    public interface ICaptchaDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
