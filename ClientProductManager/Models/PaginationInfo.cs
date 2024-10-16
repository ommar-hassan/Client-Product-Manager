namespace ClientProductManager.Models
{
    public class PaginationInfo
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }

        public PaginationInfo(int pageNumber, int pageSize, int totalCount)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize < 1 ? 10 : pageSize;
            TotalPages = (int)Math.Ceiling((double)totalCount / PageSize);
        }
    }
}
