namespace Charity.Extensions
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;
    using System.IO;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class ValidateCaptchaAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Request.EnableBuffering();
            try
            {
                string inputText = null;
                if (context.HttpContext.Request.HasFormContentType)
                {
                    inputText = context.HttpContext.Request.Form["Captcha"];
                }
                else if (context.HttpContext.Request.ContentType != null && (context.HttpContext.Request.ContentType.ToLower() == "application/json" || context.HttpContext.Request.ContentType.ToLower() == "application/json;charset=utf-8"))
                {
                    context.HttpContext.Request.Body.Seek(0, SeekOrigin.Begin);
                    using StreamReader ms = new StreamReader(context.HttpContext.Request.Body);
                    var bodyString = ms.ReadToEnd();
                    dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(bodyString);
                    inputText = data.Captcha;
                }
                else
                {
                    throw new Exception("خطایی اتفاق افتاده است");
                }

                ValidateCaptchaHelper.Instance.Execute(context.HttpContext, inputText);
            }
            catch (Exception ex)
            {
                context.ModelState.AddModelError("Captcha", ex.Message);
            }
        }
    }
}
