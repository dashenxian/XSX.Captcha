using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XSX.Captcha
{
    public class CaptchaGenerateAppService : CaptchaAppService
    {
        private readonly ICaptchaGenerate captchaGenerate;

        public CaptchaGenerateAppService(ICaptchaGenerate captchaGenerate)
        {
            this.captchaGenerate = captchaGenerate;
        }
        /// <summary>
        /// 验证验证码
        /// </summary>
        /// <param name="userInputCaptcha">用户输入验证码</param>
        /// <returns></returns>
        public async Task<bool> ValidateCaptchaCode(string userInputCaptcha)
        {
            return await captchaGenerate.ValidateCaptchaCodeWithUserName(userInputCaptcha);
        }
        /// <summary>
        /// 验证验证码
        /// </summary>
        /// <param name="captchaKey">验证码的key，查找指定验证码</param>
        /// <param name="userInputCaptcha">用户输入验证码</param>
        /// <returns></returns>
        public async Task<bool> ValidateCaptchaCode(string captchaKey, string userInputCaptcha)
        {
            return await captchaGenerate.ValidateCaptchaCode(captchaKey, userInputCaptcha);
        }
        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="width">图形宽度</param>
        /// <param name="height">图形高度</param>
        /// <returns>验证码数据</returns>
        public async Task<CaptchaResult> GenerateCaptchaImage(int width, int height)
        {
            return await captchaGenerate.GenerateCaptchaImageWithUserName(width, height);
        }
        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="width">图形宽度</param>
        /// <param name="height">图形高度</param>
        /// <param name="captchaCode">验证码内容</param>
        /// <returns>验证码数据</returns>
        public async Task<CaptchaResult> GenerateCaptchaImage(string captchaKey, int width, int height)
        {
            return await captchaGenerate.GenerateCaptchaImage(captchaKey, width, height);
        }
    }
}
