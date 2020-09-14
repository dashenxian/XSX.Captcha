using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Users;

namespace XSX.Captcha
{
    public class CaptchaGenerate : ICaptchaGenerate, ITransientDependency
    {

        const string Letters = "2346789ABCDEFGHJKLMNPRTUVWXYZ";
        private readonly IDistributedCache<string> distributedCache;
        private readonly ICurrentUser currentUser;

        public CaptchaGenerate(IDistributedCache<string> distributedCache, ICurrentUser currentUser)
        {
            this.distributedCache = distributedCache;
            this.currentUser = currentUser;
        }
        public string GenerateCaptchaCode()
        {
            Random rand = new Random();
            int maxRand = Letters.Length - 1;

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < 4; i++)
            {
                int index = rand.Next(maxRand);
                sb.Append(Letters[index]);
            }

            return sb.ToString();
        }
        /// <summary>
        /// 验证验证码
        /// </summary>
        /// <param name="userInputCaptcha">用户输入验证码</param>
        /// <returns></returns>
        public async Task<bool> ValidateCaptchaCodeWithUserName(string userInputCaptcha)
        {
            if (currentUser == null)
            {
                return false;
            }
            return await ValidateCaptchaCode(currentUser.UserName, userInputCaptcha);
        }
        /// <summary>
        /// 验证验证码
        /// </summary>
        /// <param name="captchaKey">验证码的key，查找指定验证码</param>
        /// <param name="userInputCaptcha">用户输入验证码</param>
        /// <returns></returns>
        public async Task<bool> ValidateCaptchaCode(string captchaKey, string userInputCaptcha)
        {
            var captcha = await distributedCache.GetAsync(captchaKey);
            var isValid = userInputCaptcha.Equals(captcha, StringComparison.OrdinalIgnoreCase);
            distributedCache.Remove(captchaKey);
            return isValid;
        }
        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="captchaKey">验证码缓存key</param>
        /// <param name="width">图形宽度</param>
        /// <param name="height">图形高度</param>
        /// <returns>验证码数据</returns>
        public async Task<CaptchaResult> GenerateCaptchaImage(string captchaKey, int width, int height)
        {
            var captchaCode = GenerateCaptchaCode();
            await distributedCache.SetAsync(captchaKey, captchaCode);
            return GenerateCaptchaImage(width, height, captchaCode);
        }
        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="width">图形宽度</param>
        /// <param name="height">图形高度</param>
        /// <returns>验证码数据</returns>
        public async Task<CaptchaResult> GenerateCaptchaImageWithUserName(int width, int height)
        {
            if (currentUser == null)
            {
                throw new AbpAuthorizationException("用户未登录");
            }
            return await GenerateCaptchaImage(currentUser.UserName, width, height);
        }
        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="width">图形宽度</param>
        /// <param name="height">图形高度</param>
        /// <param name="captchaCode">验证码内容</param>
        /// <returns>验证码数据</returns>
        private CaptchaResult GenerateCaptchaImage(int width, int height, string captchaCode)
        {
            using Bitmap baseMap = new Bitmap(width, height);
            using Graphics graph = Graphics.FromImage(baseMap);

            Random rand = new Random();

            graph.Clear(GetRandomLightColor());

            DrawCaptchaCode();
            DrawDisorderLine();
            AdjustRippleEffect();

            MemoryStream ms = new MemoryStream();

            baseMap.Save(ms, ImageFormat.Png);

            return new CaptchaResult { CaptchaCode = captchaCode, CaptchaByteData = ms.ToArray(), Timestamp = DateTime.Now };

            int GetFontSize(int imageWidth, int captchCodeCount)
            {
                var averageSize = imageWidth / captchCodeCount;

                return Convert.ToInt32(averageSize);
            }

            Color GetRandomDeepColor()
            {
                int redlow = 160, greenLow = 100, blueLow = 160;
                return Color.FromArgb(rand.Next(redlow), rand.Next(greenLow), rand.Next(blueLow));
            }

            Color GetRandomLightColor()
            {
                int low = 180, high = 255;

                int nRend = rand.Next(high) % (high - low) + low;
                int nGreen = rand.Next(high) % (high - low) + low;
                int nBlue = rand.Next(high) % (high - low) + low;

                return Color.FromArgb(nRend, nGreen, nBlue);
            }

            void DrawCaptchaCode()
            {
                SolidBrush fontBrush = new SolidBrush(Color.Black);
                int fontSize = GetFontSize(width, captchaCode.Length);
                Font font = new Font(FontFamily.GenericSerif, fontSize, FontStyle.Bold, GraphicsUnit.Pixel);
                for (int i = 0; i < captchaCode.Length; i++)
                {
                    fontBrush.Color = GetRandomDeepColor();

                    int shiftPx = fontSize / 6;

                    float x = i * fontSize + rand.Next(-shiftPx, shiftPx) + rand.Next(-shiftPx, shiftPx);
                    int maxY = height - fontSize;
                    if (maxY < 0) maxY = 0;
                    float y = rand.Next(0, maxY);

                    graph.DrawString(captchaCode[i].ToString(), font, fontBrush, x, y);
                }
            }

            void DrawDisorderLine()
            {
                Pen linePen = new Pen(new SolidBrush(Color.Black), 3);
                for (int i = 0; i < rand.Next(3, 5); i++)
                {
                    linePen.Color = GetRandomDeepColor();

                    Point startPoint = new Point(rand.Next(0, width), rand.Next(0, height));
                    Point endPoint = new Point(rand.Next(0, width), rand.Next(0, height));
                    graph.DrawLine(linePen, startPoint, endPoint);

                    //Point bezierPoint1 = new Point(rand.Next(0, width), rand.Next(0, height));
                    //Point bezierPoint2 = new Point(rand.Next(0, width), rand.Next(0, height));

                    //graph.DrawBezier(linePen, startPoint, bezierPoint1, bezierPoint2, endPoint);
                }
            }

            void AdjustRippleEffect()
            {
                short nWave = 6;
                int nWidth = baseMap.Width;
                int nHeight = baseMap.Height;

                Point[,] pt = new Point[nWidth, nHeight];

                for (int x = 0; x < nWidth; ++x)
                {
                    for (int y = 0; y < nHeight; ++y)
                    {
                        var xo = nWave * Math.Sin(2.0 * 3.1415 * y / 128.0);
                        var yo = nWave * Math.Cos(2.0 * 3.1415 * x / 128.0);

                        var newX = x + xo;
                        var newY = y + yo;

                        if (newX > 0 && newX < nWidth)
                        {
                            pt[x, y].X = (int)newX;
                        }
                        else
                        {
                            pt[x, y].X = 0;
                        }


                        if (newY > 0 && newY < nHeight)
                        {
                            pt[x, y].Y = (int)newY;
                        }
                        else
                        {
                            pt[x, y].Y = 0;
                        }
                    }
                }

                Bitmap bSrc = (Bitmap)baseMap.Clone();

                BitmapData bitmapData = baseMap.LockBits(new Rectangle(0, 0, baseMap.Width, baseMap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                BitmapData bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

                int scanline = bitmapData.Stride;

                IntPtr scan0 = bitmapData.Scan0;
                IntPtr srcScan0 = bmSrc.Scan0;

                unsafe
                {
                    byte* p = (byte*)(void*)scan0;
                    byte* pSrc = (byte*)(void*)srcScan0;

                    int nOffset = bitmapData.Stride - baseMap.Width * 3;

                    for (int y = 0; y < nHeight; ++y)
                    {
                        for (int x = 0; x < nWidth; ++x)
                        {
                            var xOffset = pt[x, y].X;
                            var yOffset = pt[x, y].Y;

                            if (yOffset >= 0 && yOffset < nHeight && xOffset >= 0 && xOffset < nWidth)
                            {
                                if (pSrc != null)
                                {
                                    p[0] = pSrc[yOffset * scanline + xOffset * 3];
                                    p[1] = pSrc[yOffset * scanline + xOffset * 3 + 1];
                                    p[2] = pSrc[yOffset * scanline + xOffset * 3 + 2];
                                }
                            }

                            p += 3;
                        }
                        p += nOffset;
                    }
                }

                baseMap.UnlockBits(bitmapData);
                bSrc.UnlockBits(bmSrc);
                bSrc.Dispose();
            }

        }
    }
}
