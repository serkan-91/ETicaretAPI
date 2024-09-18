namespace EticaretAPI.Application.RequestParameters;
public class PagingResult<T> where T : class
	{
	public List<T> Items { get; set; }
	public int TotalCount { get; set; }
	}
