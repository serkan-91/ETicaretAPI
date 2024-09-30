namespace EticaretAPI.Application.RequestParameters;

public record Pagination
{
    public int PageNumber { get; set; } = 0;
    public int PageSize { get; set; } = 5;
}
