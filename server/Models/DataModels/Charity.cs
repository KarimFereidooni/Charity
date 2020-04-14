using System.Collections.Generic;

namespace Charity.Models.DataModels
{
    public class Charity
    {
        public Charity()
        {
            this.Tags = new HashSet<CharityTag>();
        }

        public Charity(int id, string name, string location)
            : this()
        {
            this.Id = id;
            this.Name = name;
            this.Location = location;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public ICollection<CharityTag> Tags { get; set; }
    }
}
