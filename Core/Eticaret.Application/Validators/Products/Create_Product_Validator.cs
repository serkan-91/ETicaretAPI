using EticaretAPI.Application.Features.Commands.Product.Create;

namespace EticaretAPI.Application.Validators.Products;

public class CreateProductCommandRequestValidator : AbstractValidator<CreateProductCommandRequest>
{
    public CreateProductCommandRequestValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("Product name must not be empty or null.")
            .Length(3, 350)
            .WithMessage("Product name must be between 3 and 350 characters.");

        RuleFor(s => s.Stock).GreaterThanOrEqualTo(0).WithMessage("Stock cannot be negative.");

        RuleFor(p => p.Price).GreaterThanOrEqualTo(0).WithMessage("Price cannot be negative.");
    }
}
