namespace Charity.Models.ViewModels
{
    public class TablePagination
    {
        public string[] SortBy { get; set; }

        public bool[] SortDesc { get; set; }

        public int Page { get; set; }

        public int ItemsPerPage { get; set; }
    }
}
