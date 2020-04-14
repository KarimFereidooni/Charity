using Microsoft.AspNetCore.Http;
using System;

namespace Charity.Extensions
{
    public class ValidateCaptchaHelper
    {
        /// <summary>
        /// پیغامی مبنی بر اینکه در رمزگشایی اطلاعات کوکی تصویر امنیتی خطایی رخ داده است.
        /// </summary>
        public string ErrorWasHappened { get; set; }

        /// <summary>
        /// پیغام مبنی بر این که وارد کردن کد تصویر امنیتی الزامی است.
        /// </summary>
        public string CaptchaCodeIsRequired { get; set; }

        /// <summary>
        /// جواب معادله وارد شده توسط کاربر صحیح نمی باشد.
        /// </summary>
        public string CaptchaCodeIsIncorrect { get; set; }

        /// <summary>
        ///  پیغام مبنی بر این که زمان ارسال فرم حاوی تصویر امنیتی به پایان رسیده است.
        /// </summary>
        public string TimeIsExpired { get; set; }

        /// <summary>
        /// پیغام مبنی بر اینکه کوکی مرورگر باید فعال باشد.
        /// </summary>
        public string CookieMustEnabled { get; set; }

        /// <summary>
        /// حداکثر زمان ممکن برای ارسال فرم حاوی تصویر امنیتی.
        /// </summary>
        public int ExpireTimeCaptchaCodeBySeconds { get; set; }

        /// <summary>
        /// کلید رمزگشایی اطلاعات، این کلید باید با کلید رمزنگاری اطلاعات که در کلاس
        /// CaptchaImageResult
        /// تعریف شده است یکسان باشد.
        /// </summary>
        private const string DecryptionKey = "d2s23dxgm";

        private static ValidateCaptchaHelper instance;

        public static ValidateCaptchaHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ValidateCaptchaHelper();
                }

                return instance;
            }
        }

        public ValidateCaptchaHelper(int expireTimeCaptchaCodeBySeconds = 10 * 60)
        {
            this.ErrorWasHappened = "خطایی اتفاق افتاده است";
            this.CaptchaCodeIsRequired = "لطفا جواب معادله را وارد کنید";
            this.TimeIsExpired = "جواب معادله منقضی شده است لطفا دوباره روی تصویر کلیک کنید تا معادله تازه شود.";
            this.CaptchaCodeIsIncorrect = "جواب معادله را اشتباه وارد کرده اید";
            this.CookieMustEnabled = "باید ابتدا قابلیت کوکی ها را در مرورگر خود فعال کنید";
            this.ExpireTimeCaptchaCodeBySeconds = expireTimeCaptchaCodeBySeconds;
        }

        public ValidateCaptchaHelper(
            string errorWasHappened,
            string captchaCodeIsRequired,
            string captchaCodeIsIncorrect,
            string timeIsExpired,
            string cookieMustEnabled,
            int expireTimeCaptchaCodeBySeconds)
        {
            this.ErrorWasHappened = errorWasHappened;
            this.CaptchaCodeIsRequired = captchaCodeIsRequired;
            this.CaptchaCodeIsIncorrect = captchaCodeIsIncorrect;
            this.TimeIsExpired = timeIsExpired;
            this.CookieMustEnabled = cookieMustEnabled;
            this.ExpireTimeCaptchaCodeBySeconds = expireTimeCaptchaCodeBySeconds;
        }

        public void Execute(HttpContext context, string inputText)
        {
            // var controllerBase = filterContext.Controller;
            // var CaptchaProvider = controllerBase.ValueProvider.GetValue("Captcha");
            if (inputText == null)
            {
                throw new Exception(this.ErrorWasHappened);
                // context.ModelState.AddModelError("Captcha", this.ErrorWasHappened);
            }

            // var inputText = CaptchaProvider.AttemptedValue;
            var httpCookie = context.Request.Cookies["captchastring"];

            if (httpCookie == null)
            {
                throw new Exception(this.CookieMustEnabled);
            }

            string decryptedString;
            try
            {
                //-- رمزگشایی کردن مقدار کوکی
                //-- با توجه به این که تاریخ فعلی (فقط روز فعلی، زمان نباید اضافه شود) موقع رمزنگاری به کلید رمزنگاری اضافه گردید
                //-- باز هم تاریخ فعلی (فقط روز فعلی، زمان نباید اضافه شود) به کلید
                //-- رمزگشایی اضافه گردیده است
                decryptedString = httpCookie.Decrypt(DecryptionKey + DateTime.Now.Date.ToString());
            }
            catch (System.Security.Cryptography.CryptographicException)
            {
                //-- خطایی در رمزگشایی اطلاعات اتفاق افتاده است، به بیانی دیگر کسی سعی در هک کردن کوکی را داشته
                //--  یا این که یک یا بیش از یک روز از زمان ارسال فرم از سرور به مرورگر کاربر گذشته است
                //--  و کاربر الآن فرم را به سرور ارسال کرده است

                //-- وجود خط زیر ضروری است، در غیر این صورت مهاجم به راحتی عملیات را انجام می دهد، بدون این که
                //-- جواب معادله را درست وارد کرده باشد، البته نوع خطا را نباید به او نشان دهید، این خطا باید در
                //-- جایی لاگ شود تا مدیر سایت بتواند آنرا بررسی کند
                throw new Exception(this.ErrorWasHappened);
            }

            string[] arr = decryptedString.Split(',');
            string originalCaptchaNumber = arr[0];
            string generatedCaptchaDateTime = arr[1];

            if (string.IsNullOrEmpty(originalCaptchaNumber) || string.IsNullOrEmpty(generatedCaptchaDateTime) || !int.TryParse(originalCaptchaNumber, out int num) ||
                !DateTime.TryParse(generatedCaptchaDateTime, out DateTime dt))
            {
                //-- اطلاعات رمزگشایی شده معتبر نیستند

                //-- وجود خط زیر ضروری است، در غیر این صورت مهاجم به راحتی عملیات را انجام می دهد، بدون این که
                //-- جواب معادله را درست وارد کرده باشد، البته نوع خطا را نباید به او نشان دهید، این خطا باید در
                //-- جایی لاگ شود تا مدیر سایت بتواند آنرا بررسی کند
                throw new Exception(this.ErrorWasHappened);
            }

            //-- به دست آوردن اختلاف زمانی بر حسب ثانیه، بین موقعی که تصویر امنیتی ایجاد شد و زمان فعلی که کاربر
            //-- جواب معادله را وارد کرده و فرم را پست کرده است
            double secondsDiff = (DateTime.Now - DateTime.Parse(generatedCaptchaDateTime)).TotalSeconds;

            //-- منقضی شدن زمان جواب معادله -- پیش فرض 30 ثانیه
            if (secondsDiff > this.ExpireTimeCaptchaCodeBySeconds)
            {
                throw new Exception(this.TimeIsExpired);
            }

            if (inputText != originalCaptchaNumber)
            {
                throw new Exception(this.CaptchaCodeIsIncorrect);
            }

            context.Response.Cookies.Delete("captchastring");
        }
    }
}
