using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XSX.Captcha.Samples
{
    public class CaptchaGenerateAppServiceTest : CaptchaApplicationTestBase
    {
        private readonly CaptchaGenerateAppService captchaGenerateAppService;

        public CaptchaGenerateAppServiceTest()
        {
            captchaGenerateAppService = GetRequiredService<CaptchaGenerateAppService>();
        }

        [Fact]
        public async Task GenerateCaptcha_Should_Validate_Success()
        {
            //Act
            var key = "captchaKey";
            var captcha = await captchaGenerateAppService.GenerateCaptchaImage(key, 300, 300);
            var result = await captchaGenerateAppService.ValidateCaptchaCode(key, captcha.CaptchaCode);

            result.ShouldBe(true);
        }
        [Fact]
        public async Task GenerateCaptchaValidateTow_Should_Validate_Failed()
        {
            //Act
            var key = "captchaKey";
            var captcha = await captchaGenerateAppService.GenerateCaptchaImage(key, 300, 300);
            await captchaGenerateAppService.ValidateCaptchaCode(key, captcha.CaptchaCode);
            var result = await captchaGenerateAppService.ValidateCaptchaCode(key, captcha.CaptchaCode);

            result.ShouldBe(false);
        }
    }
}
