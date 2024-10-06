using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EticaretAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using D = EticaretAPI.Domain.Entities.Identity;

namespace EticaretAPI.Application.Features.Commands.ApplicationUser.Login;

public class LoginUserCommandHandler(
	UserManager<D.ApplicationUser> UserManager,
	SignInManager<D.ApplicationUser> SignInManager
) : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
{
	async Task<LoginUserCommandResponse> IRequestHandler<
		LoginUserCommandRequest,
		LoginUserCommandResponse
	>.Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
	{
		var user = await UserManager.FindByNameAsync(request.Username);
		if (user == null)
			user = await UserManager.FindByEmailAsync(request.Username);

		if (user == null)
			throw new ApiException("Kullanici Bulunamadi.");

		SignInResult result = await SignInManager.CheckPasswordSignInAsync(
			user,
			request.Password,
			false
		);
		if (result == SignInResult.Success)
		{
			return new();
		}
		else
		{
			throw new ApiException("Kullanici adi veya sifre hatali.");
		}
	}
}
