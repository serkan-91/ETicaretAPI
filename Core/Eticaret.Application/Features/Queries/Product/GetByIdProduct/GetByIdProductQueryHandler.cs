using System;
using System.Linq;
using AutoMapper;
using EticaretAPI.Application.Operations;
using MediatR;
using E = EticaretAPI.Domain.Entities;

namespace EticaretAPI.Application.Features.Queries.Product.GetByIdProduct
{
    public class GetByIdProductQueryHandler(IProductServices _productService,IMapper _mapper)
        : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
    { 

        public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request , CancellationToken cancellationToken)
        {
         E.Product  product =  await _productService
                 .GetProductByIdAsync(request.Id , cancellationToken)
                 .ConfigureAwait(false);
        return _mapper.Map<GetByIdProductQueryResponse>(product); 
        }
    }
}
