using AutoMapper;
using EticaretAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using I = EticaretAPI.Domain.Entities.Identity;

namespace EticaretAPI.Application.Features.Commands.ApplicationUser.Register;
public class CreateUserCommandHandler(UserManager<I.ApplicationUser> UserManager , IMapper Mapper) : IRequestHandler<CreateUserCommandRequest , CreateUserCommandResponse> {

	public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request , CancellationToken cancellationToken)
	{
		var user = Mapper.Map<I.ApplicationUser>(request);
		user.PasswordHash = UserManager.PasswordHasher.HashPassword(user , request.Password);
		IdentityResult result = await UserManager.CreateAsync(user , request.Password);
		if (result.Succeeded)
			return new()
			{
				Succeeded = true ,
				Message = "Kullanici Basariyla Olusturuldu."
			};
		else
		{
			var errorMessage = "Kullanici olusturulamadi. Hata: " + string.Join(", " , result.Errors.Select(x => x.Description));
			throw new ApiException(errorMessage);
		}
	}
}
