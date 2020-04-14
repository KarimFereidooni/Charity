namespace Charity.Models.ViewModels.User
{
    using System.ComponentModel.DataAnnotations;

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "{0} باید حداقل {2} و حداکثر {1} حرف باشد.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "رمزعبور جدید")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تایید رمزعبور جدید")]
        [Compare("NewPassword", ErrorMessage = "رمزعبور جدید و تایید آن باید با یکدیگر برابر باشند.")]
        public string ConfirmPassword { get; set; }
    }
}
