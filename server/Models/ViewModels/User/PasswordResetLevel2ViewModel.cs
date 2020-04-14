using System.ComponentModel.DataAnnotations;

namespace Charity.Models.ViewModels.User
{
    public class PasswordResetLevel2ViewModel
    {
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} را وارد کنید")]
        [Display(Name = "کد اهراز هویت")]
        public string Code { get; set; }

        [Required(ErrorMessage = "{0} را وارد کنید")]
        [Display(Name = "رمزعبور جدید")]
        [StringLength(100, ErrorMessage = "{0} باید حداقل {2} و حداکثر {1} حرف باشد.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "{0} را وارد کنید")]
        [Compare("NewPassword", ErrorMessage = "رمزعبور جدید و تایید آن باید با یکدیگر برابر باشند.")]
        [Display(Name = "تکرار رمزعبور جدید")]
        public string NewPasswordConfirm { get; set; }
    }
}
