namespace Charity.Extensions
{
    using Microsoft.AspNetCore.Http.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// کلاسی جهت ایجاد تصویر امنیتی در MVC.
    /// </summary>
    public class CaptchaImageResult : ActionResult
    {
        /// <summary>
        /// ارتفاع تصویر امنیتی.
        /// </summary>
        private const int Height = 60;

        /// <summary>
        /// عرض تصویر امنیتی.
        /// </summary>
        private const int Width = 230;

        /// <summary>
        /// رنگ پس زمینه تصویر امنیتی.
        /// </summary>
        private readonly Color backGroundColor = Color.FromArgb(255, 239, 239, 239);

        /// <summary>
        /// کلید رمزنگاری اطلاعات، این کلید باید با کلید رمزگشایی اطلاعات که در کلاس
        /// ValidateCaptchaAttribute
        /// تعریف شده است یکسان باشد.
        /// </summary>
        private const string EncryptionKey = "d2s23dxgm";

        /// <summary>
        /// نوع قلم تصویر امنیتی.
        /// </summary>
        private const string CaptchaFontFamily = "Tahoma";

        /// <summary>
        /// اندازه قلم تصویر امنیتی.
        /// </summary>
        private const int CaptchaFontSize = 10;

        public override void ExecuteResult(ActionContext context)
        {
            this.ExecuteResultAsync(context).Wait();
        }

        public override async Task ExecuteResultAsync(ActionContext context)
        {
            //-- ایجاد یک تصویر نقشه بیتی 32 بیتی
            Bitmap bitmap = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);

            //-- ایجاد یک شیء گرافیکی برای عملیات ترسیم روی تصویر امنیتی
            Graphics gfxCaptchaImage = Graphics.FromImage(bitmap);
            gfxCaptchaImage.PageUnit = GraphicsUnit.Pixel;
            gfxCaptchaImage.SmoothingMode = SmoothingMode.HighQuality;

            //-- پاک کردن پس زمینه تصویر امنیتی با یک رنگ سفید
            gfxCaptchaImage.Clear(this.backGroundColor);

            //-- ایجاد یک عدد اتفاقی بین 1000 و 9999
            // int salt = CaptchaHelpers.CreateSalt();

            //-- تاریخ فعلی (فقط روز فعلی) باید به کلید رمزنگاری اطلاعات اضافه شود
            //-- این کار به این هدف متفاوت بودن کلید رمزنگاری اطلاعات در هر روز صورت گرفته است
            string encryptionSaltKey = EncryptionKey + DateTimeOffset.Now.Date.ToString();

            //-- چسباندن عدد اتفاقی تولید شده به تاریخ و زمان فعلی با یک جدا کننده
            //-- توضیح: تاریخ و زمان فعلی باید در کورکی ذخیره شود تا هنگام رمزگشایی
            //-- اطلاعات، اختلاف زمانی فی ما بین زمان ارسال فرم به مرورگر کاربر و زمان
            //-- ارسال فرم به سرور توسط کاربر محاسبه شود
            Random random = new Random();
            int a = random.Next(10, 90);
            int b = random.Next(1, 9);
            string plainText = (a + b).ToString() + "," + DateTimeOffset.Now.ToString();

            //-- رمزنگاری مقدار متغیر بالا جهت ذخیره در کوکی
            string encryptedValue = plainText.Encrypt(encryptionSaltKey);

            //-- تبدیل عدد اتفاقی تولید شده به حروف معادل
            string randomString = a.ToString() + " + " + b.ToString();

            //-- تنظیمات فرمت متن تصویر امنیتی
            var format = new StringFormat();
            int faLCID = new System.Globalization.CultureInfo("fa-IR").LCID;
            format.SetDigitSubstitution(faLCID, StringDigitSubstitute.National);
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            format.FormatFlags = StringFormatFlags.DirectionRightToLeft;

            //-- نوع و اندازه قلم تصویر امنیتی
            Font font = new Font(CaptchaFontFamily, CaptchaFontSize);

            //-- ایجاد یک مسیر گرافیکی در تصویر امنیتی
            GraphicsPath path = new GraphicsPath();

            path.AddString(
                randomString,
                font.FontFamily,
                (int)font.Style,
                gfxCaptchaImage.DpiY * font.SizeInPoints / 72,
                new Rectangle(0, 0, Width, Height),
                format);

            //-- ایجاد رنگ متن تصویر امنیتی به صورت اتفاقی
            Pen pen = new Pen(Color.FromArgb(random.Next(0, 100), random.Next(0, 100), random.Next(0, 100)));
            gfxCaptchaImage.DrawPath(pen, path);

            //-- ایجاد یک موج سینوسی و کسینوسی اتفاقی برای نوشتن حروف کد امنیتی در آن
            int distortion = random.Next(-10, 10);
            using (Bitmap copy = (Bitmap)bitmap.Clone())
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        int newX = (int)(x + (distortion * Math.Sin(Math.PI * y / 64.0)));
                        int newY = (int)(y + (distortion * Math.Cos(Math.PI * x / 64.0)));
                        if (newX < 0 || newX >= Width)
                        {
                            newX = 0;
                        }

                        if (newY < 0 || newY >= Height)
                        {
                            newY = 0;
                        }

                        bitmap.SetPixel(x, y, copy.GetPixel(newX, newY));
                    }
                }
            }

            //-- اضافه کردن نویز به تصویر امنیتی
            int i, r, xx, yy, u, v;
            for (i = 1; i < 10; i++)
            {
                pen.Color = Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
                r = random.Next(0, Width / 3);
                xx = random.Next(0, Width);
                yy = random.Next(0, Height);
                u = xx - r;
                v = yy - r;
                gfxCaptchaImage.DrawEllipse(pen, u, v, r, r);
            }

            //-- رسم تصویر امنیتی
            gfxCaptchaImage.DrawImage(bitmap, new Point(0, 0));
            gfxCaptchaImage.Flush();

            //-- خروجی به عنوان یک تصویر jpeg و به صورت جریان به مرورگر کاربر فرستاده می شود
            var response = context.HttpContext.Response;
            response.ContentType = "application/json";
            response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            response.Headers["Expires"] = "-1";
            response.Headers["Pragma"] = "no-cache";

            // response.Cookies.Append("captchastring", encryptedValue, new CookieOptions() { HttpOnly = false });
            response.Headers["X-Request-URL"] = context.HttpContext.Request.GetDisplayUrl();
            response.Headers["Set-Cookie"] = $"captchastring={encryptedValue}; Path=/";

            // context.HttpContext.DisableBrowserCache();
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Jpeg);
                var base64String = "data:image/jpeg;base64," + Convert.ToBase64String(ms.ToArray());
                var newData = System.Text.Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(new { Data = base64String }, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                }));
                await response.Body.WriteAsync(newData, 0, newData.Length);
            }

            //-- آزاد سازی حافظه های اشغال شده
            font.Dispose();
            gfxCaptchaImage.Dispose();
            bitmap.Dispose();
            base.ExecuteResult(context);
        }
    }

    public static class CaptchaHelpers
    {
        public static int CreateSalt()
        {
            Random random = new Random();
            return random.Next(1000, 9999);
        }
    }
}
