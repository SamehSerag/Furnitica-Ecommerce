namespace AngularAPI.Models
{
    public class CategorySearchModel
    {
        //property to sort by 
        public string? Sort { get; set; }
        //search string
        public string? Search { get; set; }
        //page index of pagination
        public int PageIndex { get; set; } = 1;
        //items per page
        public int PageSize { get; set; } = 10;
    }
}
