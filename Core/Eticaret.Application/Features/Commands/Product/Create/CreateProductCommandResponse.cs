using System;
using System.Linq;

namespace EticaretAPI.Application.Features.Commands.Product.Create;

public class CreateProductCommandResponse
{
    public string Name { get; set; }
    public int Stock { get; set; }
    public float Price { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}
