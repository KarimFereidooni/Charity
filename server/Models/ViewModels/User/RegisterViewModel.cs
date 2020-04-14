namespace Charity.Models.ViewModels.User
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterViewModel
    {
        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Display(Name = "نام")]
        public string Name { get; set; }

        [Display(Name = "نام خانوادگی")]
        public string Surname { get; set; }

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Email { get; set; }

        [Display(Name = "شماره موبایل")]
        [StringLength(11, ErrorMessage = "{0} باید {1} رقم باشد.", MinimumLength = 11)]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string PhoneNumber { get; set; }

        // [Required(ErrorMessage = "{0} را وارد کنید")]
        [StringLength(100, ErrorMessage = "{0} باید حداقل {2} و حداکثر {1} حرف باشد.", MinimumLength = 0)]
        [DataType(DataType.Password)]
        [Display(Name = "رمزعبور")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تایید رمزعبور")]
        [Compare("Password", ErrorMessage = "رمزعبور و تایید آن باید با یکدیگر برابر باشند.")]
        public string PasswordConfirm { get; set; }

        [Display(Name = "کد امنیتی")]
        public string Captcha { get; set; }

        [Required(ErrorMessage = "{0} را انتخاب کنید")]
        [Display(Name = "نقش ها")]
        public string SelectedRole { get; set; }
    }
}
