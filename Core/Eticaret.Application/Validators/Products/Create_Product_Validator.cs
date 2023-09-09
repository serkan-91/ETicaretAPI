using EticaretAPI.Application.ViewModels.Products; 
using FluentValidation; 

namespace EticaretAPI.Application.Validators.Products
{
    public class Create_Product_Validator : AbstractValidator<VM_Create_Product>
    {
        public Create_Product_Validator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                    .WithMessage("product name must not be empty or null")
                .MaximumLength(350)
                .MinimumLength(3)
                    .WithMessage("Product name must be between 3 or 350 character");

            RuleFor(s => s.Stock)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Stock must not be empty or null")
                .Must(s => s >= 0)
                    .WithMessage("Stock not be negative");
            
            RuleFor(p => p.Price)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Price must not be empty or null")
                .Must(p => p >= 0)
                    .WithMessage("Price not be negative");

        }
    }
}
