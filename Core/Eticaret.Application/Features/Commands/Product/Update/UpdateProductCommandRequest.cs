using System;
using System.Linq;
using MediatR;

namespace EticaretAPI.Application.Features.Commands.Product.Update;

public class UpdateProductCommandRequest : IRequest<UpdateProductCommandResponse>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Stock { get; set; }
    public float Price { get; set; }
}
