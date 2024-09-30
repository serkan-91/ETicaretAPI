using System;
using System.Linq;
using MediatR;

namespace EticaretAPI.Application.Features.Commands.Product.Create;

public class CreateProductCommandRequest : IRequest<CreateProductCommandResponse>
{
    public string Name { get; set; }
    public int Stock { get; set; }
    public float Price { get; set; }
}
