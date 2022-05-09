namespace AngularAPI.Dtos
{
    public class PaginationMetaData
    {
        private const int _maxItemsPerPage = 50;

        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public int TotalCounts { get; private set; }
        public int PageSize { get; private set; }
        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

        public PaginationMetaData(int itemsCount, int pageIndex, int pageSize)
        {
            PageSize = pageSize = pageSize > _maxItemsPerPage ? _maxItemsPerPage : pageSize;

            TotalCounts = itemsCount;
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(itemsCount / (double)pageSize);
        }



        //public static async Task<IQueryable<T>> CreatePaginationListAsync(IQueryable<T> source, int pageIndex, int pageSize)
        //{
        //    return source.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        //}
    }
}
