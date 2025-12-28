using Microsoft.AspNetCore.Identity;

namespace JwtTokenSample.Models
{
	public class User : IdentityUser
	{
		public string? Name { get; set; }
	}
}
