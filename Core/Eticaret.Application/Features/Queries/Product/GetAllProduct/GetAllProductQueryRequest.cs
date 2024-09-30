using System;
using System.Linq;
using MediatR;

namespace EticaretAPI.Application.Features.Queries.Product.GetAllProduct;

public class GetAllProductQueryRequest : IRequest<GetAllProductQueryResponse>
{
    public int PageNumber { get; set; } = 0; // Varsayılan değer
    public int PageSize { get; set; } = 5; // Varsayılan değer
}
