namespace EticaretAPI.Application.RequestParameters;

public record Paginations
	{
	public int Page { get; set; } = 0;
	public int Size { get; set; } = 5;
	}
