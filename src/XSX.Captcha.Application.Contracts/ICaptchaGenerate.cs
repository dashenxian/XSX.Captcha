using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace XSX.Captcha
{
    /// <summary>
    /// 验证码接口
    /// </summary>
    public interface ICaptchaGenerate:ITransientDependency
    {
        /// <summary>
        /// 验证验证码
        /// </summary>
        /// <param name="userInputCaptcha">用户输入验证码</param>
        /// <returns></returns>
        Task<bool> ValidateCaptchaCodeWithUserName(string userInputCaptcha);
        /// <summary>
        /// 验证验证码
        /// </summary>
        /// <param name="captchaKey">验证码的key，查找指定验证码</param>
        /// <param name="userInputCaptcha">用户输入验证码</param>
        /// <returns></returns>
        Task<bool> ValidateCaptchaCode(string captchaKey, string userInputCaptcha);
        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="width">图形宽度</param>
        /// <param name="height">图形高度</param>
        /// <returns>验证码数据</returns>
        Task<CaptchaResult> GenerateCaptchaImageWithUserName(int width, int height);
        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="width">图形宽度</param>
        /// <param name="height">图形高度</param>
        /// <param name="captchaCode">验证码内容</param>
        /// <returns>验证码数据</returns>
        Task<CaptchaResult> GenerateCaptchaImage(string captchaKey, int width, int height);
    }
}
