using EticaretAPI.Application.Repositories;
using EticaretAPI.Application.RequestParameters;
using MediatR;

namespace EticaretAPI.Application.Features.Queries.Product.GetAllProduct;

public class GetAllProductQueryHandler(IProductReadRepository _productReadRepository)
    : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
{
    public async Task<GetAllProductQueryResponse> Handle(
        GetAllProductQueryRequest request,
        CancellationToken cancellationToken
    ) =>
        new()
        {
            PagingResult = await _productReadRepository.GetPagedAsync(
                cancellationToken,
                new Pagination() { PageNumber = request.PageNumber, PageSize = request.PageSize },
                false
            ),
        };
}
