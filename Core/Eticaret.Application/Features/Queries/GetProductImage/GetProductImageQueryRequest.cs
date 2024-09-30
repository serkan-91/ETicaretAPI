using MediatR;

namespace EticaretAPI.Application.Features.Queries.GetProductImage;

public class GetProductImageQueryRequest : IRequest<List<GetProductImageQueryResponse>>
{
	public string Id { get; set; }
}
