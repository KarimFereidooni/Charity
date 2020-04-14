namespace Charity.Models.ViewModels.User
{
    using System;

    public class MinimumUserInfo
    {
        public MinimumUserInfo()
        {
        }

        public MinimumUserInfo(int id, string userName, string fullName, string avatar, DateTimeOffset? loginDateTime)
            : this()
        {
            this.Id = id;
            this.UserName = userName;
            this.Avatar = avatar;
            this.FullName = fullName;
            this.LoginDateTime = loginDateTime;
        }

        public MinimumUserInfo(Models.DataModels.UserModels.User user)
            : this(
                  user.Id,
                  user.UserName,
                  user.FullName,
                  user.Avatar,
                  user.LoginDateTime)
        {
        }

        public int Id { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public string Avatar { get; set; }

        public DateTimeOffset? LoginDateTime { get; set; }
    }
}
