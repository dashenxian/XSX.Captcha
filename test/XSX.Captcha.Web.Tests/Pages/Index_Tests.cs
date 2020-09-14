using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace XSX.Captcha.Pages
{
    public class Index_Tests : CaptchaWebTestBase
    {
        [Fact]
        public async Task Welcome_Page()
        {
            var response = await GetResponseAsStringAsync("/");
            response.ShouldNotBeNull();
        }
    }
}
