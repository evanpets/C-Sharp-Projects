using FluentValidation;
using ProductsWebDBApp.DTO;

namespace ProductsWebDBApp.Validators
{
	public class ProductInsertValidator : AbstractValidator<ProductInsertDTO>
	{
		public ProductInsertValidator()
		{
            RuleFor(p => p.Title)
                .NotEmpty()
                .WithMessage("Title cannot be empty")
                .Length(2, 50)
                .WithMessage("Title must be between 2 and 50 characters");

            RuleFor(p => p.Quantity)
                .NotEmpty()
                .WithMessage("Quantity cannot be empty")
                .GreaterThanOrEqualTo(0)
                .WithMessage("Quantity cannot be negative");

            RuleFor(p => p.Price)
                .NotEmpty()
                .WithMessage("Price cannot be empty")
                .GreaterThanOrEqualTo(0)
                .WithMessage("Price cannot be negative");
        }
	}
}
