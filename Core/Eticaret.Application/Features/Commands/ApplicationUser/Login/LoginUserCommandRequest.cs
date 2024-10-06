using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Features.Commands.ApplicationUser.Login;
public class LoginUserCommandRequest: IRequest<LoginUserCommandResponse>{
	public  string Username { get; set; }
	public  string Password { get; set; }
}
