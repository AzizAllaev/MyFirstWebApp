using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using WebApplication1.Models;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace WebApplication1.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class JwtTokenController : ControllerBase
	{
		private readonly NorthwindContext _context;
		public JwtTokenController(NorthwindContext context)
		{
			_context = context;
		}

		[HttpPost]
		public async Task<IActionResult> Login([FromBody] UserLogin user)
		{
			if (!await _context.Users.AnyAsync(u => u.Name == user.Username && u.Login == user.Password))
				return Unauthorized();
			string token = GenerateJwtToken(user.Username);

			return Ok(new { token });
		}

		private string GenerateJwtToken(string username)
		{
			var claims = new[]
			{
				new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, username),
				new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secretkey"));
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
