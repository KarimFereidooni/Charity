namespace Charity.Models.ViewModels.User
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class GoogleLoginViewModel
    {
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "{0} مشخص نیست")]
        public string Email { get; set; }

        [Display(Name = "نام")]
        public string GivenName { get; set; }

        [Display(Name = "نام خانوادگی")]
        public string FamilyName { get; set; }

        [Display(Name = "آدرس تصویر")]
        public Uri ImageUrl { get; set; }

        [Display(Name = "Google Id")]
        [Required(ErrorMessage = "{0} مشخص نیست")]
        public string GoogleId { get; set; }
    }
}
