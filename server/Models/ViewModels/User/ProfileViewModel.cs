namespace Charity.Models.ViewModels.User
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ProfileViewModel
    {
        public ProfileViewModel()
        {
        }

        public ProfileViewModel(int? id, bool isAuthenticated, string userName, string title, string name, string surname, string email, string phoneNumber, string avatar, string nationCode, string idNumber, string homePhoneNumber, string workPhoneNumber, string fax, string address, string postalCode, string bankAccountNumber, string bankName, string bankCard, string bankIBAN, string iDPayLink, string fullName, bool disabled, string chatId, DateTimeOffset? lastLoginDateTime, DateTimeOffset? loginDateTime, UserRoleViewModel[] roles, string token)
            : this()
        {
            this.Id = id ?? -1;
            this.IsAuthenticated = isAuthenticated;
            this.UserName = userName;
            this.Title = title;
            this.Name = name;
            this.Surname = surname;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.Avatar = avatar;
            this.NationCode = nationCode;
            this.IdNumber = idNumber;
            this.HomePhoneNumber = homePhoneNumber;
            this.WorkPhoneNumber = workPhoneNumber;
            this.Fax = fax;
            this.Address = address;
            this.PostalCode = postalCode;
            this.BankAccountNumber = bankAccountNumber;
            this.BankName = bankName;
            this.BankCard = bankCard;
            this.BankIBAN = bankIBAN;
            this.IDPayLink = iDPayLink;
            this.FullName = fullName;
            this.Disabled = disabled;
            this.ChatId = chatId;
            this.LastLoginDateTime = lastLoginDateTime;
            this.LoginDateTime = loginDateTime;
            this.Roles = roles;
            this.Token = token;
        }

        public ProfileViewModel(Models.DataModels.UserModels.User user, bool isAuthenticated, Models.ViewModels.User.UserRoleViewModel[] roles, string token)
            : this(
                  user?.Id,
                  isAuthenticated,
                  user?.UserName,
                  user?.Title,
                  user?.Name,
                  user?.Surname,
                  user?.Email,
                  user?.PhoneNumber,
                  user?.Avatar,
                  user?.NationCode,
                  user?.IdNumber,
                  user?.HomePhoneNumber,
                  user?.WorkPhoneNumber,
                  user?.Fax,
                  user?.Address,
                  user?.PostalCode,
                  user?.BankAccountNumber,
                  user?.BankName,
                  user?.BankCard,
                  user?.BankIBAN,
                  user?.IDPayLink,
                  user?.FullName,
                  user == null ? false : user.Disabled,
                  user?.ChatId,
                  user?.LastLoginDateTime,
                  user?.LoginDateTime,
                  roles,
                  token)
        {
        }

        public int Id { get; set; }

        public bool IsAuthenticated { get; set; }

        [Required(ErrorMessage = "{0} را وارد کنید")]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        public string Title { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        [Required(ErrorMessage = "{0} را وارد کنید")]
        [Display(Name = "شماره همراه")]
        public string PhoneNumber { get; set; }

        public string Avatar { get; set; }

        public string NationCode { get; set; }

        public string IdNumber { get; set; }

        public string HomePhoneNumber { get; set; }

        public string WorkPhoneNumber { get; set; }

        public string Fax { get; set; }

        public string Address { get; set; }

        public string PostalCode { get; set; }

        public string BankAccountNumber { get; set; }

        public string BankName { get; set; }

        public string BankCard { get; set; }

        public string BankIBAN { get; set; }

        public string IDPayLink { get; set; }

        public string FullName { get; set; }

        public bool Disabled { get; set; }

        public string ChatId { get; set; }

        public string Token { get; set; }

        public DateTimeOffset? LastLoginDateTime { get; set; }

        public DateTimeOffset? LoginDateTime { get; set; }

        public UserRoleViewModel[] Roles { get; set; }
    }
}
