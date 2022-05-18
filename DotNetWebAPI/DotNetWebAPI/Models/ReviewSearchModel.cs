namespace DotNetWebAPI.Models
{
    public class ReviewSearchModel
    {
        //property to sort by 
        public string? Sort { get; set; }
        //search string
        public int PageIndex { get; set; } = 1;
        //items per page

        public int PageSize { get; set; } = 10;
        public int? PrdId { get; set; }      

        public int? stars { get; set; }
    }
}
