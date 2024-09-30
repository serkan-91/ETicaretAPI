using System;
using System.Linq;
using AutoMapper;
using EticaretAPI.Application.Repositories;
using MediatR;
using ProductEntity = EticaretAPI.Domain.Entities.Product;

namespace EticaretAPI.Application.Features.Commands.Product.Create;

public class CreateProductCommandHandler(
    IProductWriteRepository _productWriteRepository,
    IMapper _mapper
) : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
{
    public async Task<CreateProductCommandResponse> Handle(
        CreateProductCommandRequest request,
        CancellationToken cancellationToken
    )
    {
        ProductEntity newProduct = _mapper.Map<ProductEntity>(request);

        await _productWriteRepository.AddAsync(newProduct);
        await _productWriteRepository.SaveChangesAsync();

        return _mapper.Map<CreateProductCommandResponse>(newProduct);
    }
}
