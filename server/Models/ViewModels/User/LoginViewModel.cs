namespace Charity.Models.ViewModels.User
{
    using System.ComponentModel.DataAnnotations;

    public class LoginViewModel
    {
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} را وارد کنید")]
        [DataType(DataType.Password)]
        [Display(Name = "رمزعبور")]
        public string Password { get; set; }

        [Display(Name = "کد امنیتی")]
        public string Captcha { get; set; }
    }
}
