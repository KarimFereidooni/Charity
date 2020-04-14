namespace Charity.Models.ViewModels.User
{
    using System.ComponentModel.DataAnnotations;

    public class ChangePasswordViewModel
    {
        [DataType(DataType.Password)]
        [Display(Name = "رمزعبور فعلی")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "{0} را وارد کنید")]
        [StringLength(100, ErrorMessage = "{0} باید حداقل {2} و حداکثر {1} حرف باشد.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "رمزعبور جدید")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "{0} را وارد کنید")]
        [DataType(DataType.Password)]
        [Display(Name = "تایید رمزعبور جدید")]
        [Compare("NewPassword", ErrorMessage = "رمزعبور جدید و تایید آن باید با یکدیگر برابر باشند.")]
        public string NewPasswordConfirm { get; set; }
    }
}
