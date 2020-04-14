namespace Charity.Models.ViewModels.User
{
    using System;

    public class ExternalLoginInfo
    {
        public ExternalLoginInfo()
        {
        }

        public ExternalLoginInfo(string email, string name, string surname, string mobilePhone, Uri imageUrl)
        {
            this.Email = email;
            this.Name = name;
            this.Surname = surname;
            this.MobilePhone = mobilePhone;
            this.ImageUrl = imageUrl;
        }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string MobilePhone { get; set; }

        public Uri ImageUrl { get; set; }

        public string Email { get; set; }
    }
}
