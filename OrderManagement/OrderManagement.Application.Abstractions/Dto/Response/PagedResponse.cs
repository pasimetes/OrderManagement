namespace OrderManagement.Application.Abstractions.Dto.Response
{
    public class PagedResponse<T>
    {
        public ICollection<T> Results { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalRecords { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);
    }
}
