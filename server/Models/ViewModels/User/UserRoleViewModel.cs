namespace Charity.Models.ViewModels.User
{
    public class UserRoleViewModel
    {
        public UserRoleViewModel()
        {
        }

        public UserRoleViewModel(long id, string name, string title)
            : this()
        {
            this.Id = id;
            this.Name = name;
            this.Title = title;
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }
    }
}
