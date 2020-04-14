using System.ComponentModel.DataAnnotations;

namespace Charity.Models.ViewModels.User
{
    public class PasswordResetViewModel
    {
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
    }
}
