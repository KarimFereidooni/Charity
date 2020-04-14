namespace Charity.Models.DataModels
{
    public class CharityTag
    {
        public CharityTag()
        {
        }

        public CharityTag(int id, int charityId, string tag)
            : this()
        {
            this.Id = id;
            this.CharityId = charityId;
            this.Tag = tag;
        }

        public int Id { get; set; }

        public int CharityId { get; set; }

        public Charity Charity { get; set; }

        public string Tag { get; set; }
    }
}
