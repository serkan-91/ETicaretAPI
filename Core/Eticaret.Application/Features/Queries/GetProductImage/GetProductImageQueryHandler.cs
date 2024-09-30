using EticaretAPI.Application.Operations;
using MediatR;

namespace EticaretAPI.Application.Features.Queries.GetProductImage;

public class GetProductImageQueryHandler(IProductServices ProductService)
	: IRequestHandler<GetProductImageQueryRequest, List<GetProductImageQueryResponse>>
{
	public async Task<List<GetProductImageQueryResponse>> Handle(
		GetProductImageQueryRequest request,
		CancellationToken cancellationToken
	)
	{
		var result = await ProductService.GetProductImagesAsync(request.Id, cancellationToken);
		return result;
	}
}
