using Microsoft.AspNetCore.Identity;

namespace EticaretAPI.Domain.Entities.Identity;

public class ApplicationUser : IdentityUser
{
	public string FullName { get; set; }
}
