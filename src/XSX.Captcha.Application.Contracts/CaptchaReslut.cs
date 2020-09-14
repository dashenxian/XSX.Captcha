using System;
using System.Collections.Generic;
using System.Text;

namespace XSX.Captcha
{

    public class CaptchaResult
    {
        /// <summary>
        /// 验证码
        /// </summary>
        public string CaptchaCode { get; set; }
        /// <summary>
        /// 验证码图片数据
        /// </summary>
        public byte[] CaptchaByteData { get; set; }
        /// <summary>
        /// Base64编码验证码图片数据
        /// </summary>
        public string CaptchBase64Data => Convert.ToBase64String(CaptchaByteData);
        /// <summary>
        /// 生成时间
        /// </summary>
        public DateTime Timestamp { get; set; }
    }

}
