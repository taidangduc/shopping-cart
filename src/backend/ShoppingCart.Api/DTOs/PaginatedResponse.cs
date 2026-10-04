namespace ShoppingCart.Api.DTOs;
    
public class PaginatedResponse<T>
{
    public IEnumerable<T> Items { get; set; }
    public int Total { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)Total / PageSize);
}