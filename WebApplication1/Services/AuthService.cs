using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtTokenSample.Service
{
	public class AuthService
	{
		public static string GenerateJwtToken(string username)
		{
			var claims = new[]
			{
				new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, username),
				new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secretkeysecretkeysecretkeysecretkeysecretkey"));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: "domain",
				audience: "domain",
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(30),
				signingCredentials: creds
				);
			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
