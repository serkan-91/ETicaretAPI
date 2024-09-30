using System;
using System.Linq;
using MediatR;

namespace EticaretAPI.Application.Features.Queries.Product.GetByIdProduct;
public class GetByIdProductQueryRequest : IRequest<GetByIdProductQueryResponse>
{
    public string Id { get; set; }

}
