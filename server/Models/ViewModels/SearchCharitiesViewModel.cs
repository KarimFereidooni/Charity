namespace Transer.Models.ViewModels
{
    public class SearchCharitiesViewModel
    {
        public string SortBy { get; set; }

        public bool Descending { get; set; }

        public int Page { get; set; }

        public int RowsPerPage { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public string Tag { get; set; }
    }
}
