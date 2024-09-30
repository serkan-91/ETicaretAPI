using System;
using System.Linq;
using EticaretAPI.Application.RequestParameters;
using E= EticaretAPI.Domain.Entities;

namespace EticaretAPI.Application.Features.Queries.Product.GetAllProduct;

public class GetAllProductQueryResponse
{
    public PagingResult<E.Product> PagingResult { get; set; }
}
