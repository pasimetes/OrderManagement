namespace OrderManagement.Application.Abstractions.Dto.Request
{
    public class PagedRequest
    {
        public string Query { get; set; }

        public int PageNumber { get; set; } = 0;

        public int PageSize { get; set; } = 20;
    }
}
