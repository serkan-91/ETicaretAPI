using EticaretAPI.Application.Features.Commands.ApplicationUser.Register;
using EticaretAPI.Application.Features.Commands.Product.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Validators.Users;
public class CreateUserCommandRequestValidator : AbstractValidator<CreateUserCommandRequest> {
	public CreateUserCommandRequestValidator() {

		RuleFor(u=> u.UserName)
			.NotEmpty()
			.WithMessage("Username must not be empty or null.")
			.Length(3 , 50)
			.WithMessage("Username must be between 3 and 50 characters.");
		RuleFor(u=> u.FullName) .
			NotEmpty()
			.WithMessage("Fullname must not be empty or null.")
			.Length(3 , 50)
			.WithMessage("Fullname must be between 3 and 50 characters.");

		RuleFor(u => u.Email)
			.NotEmpty()
			.WithMessage("Email must not be empty or null.")
			.EmailAddress()
			.WithMessage("Email is not valid.");

		RuleFor(u => u.Password)
			.NotEmpty()
			.WithMessage("Password must not be empty or null.")
			.Length(6 , 50)
			.WithMessage("Password must be between 6 and 50 characters.")
			.Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,50}$")
			.WithMessage("Password must contain at least one uppercase letter, one lowercase letter and one number.");






	}



}
