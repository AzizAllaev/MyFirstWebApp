using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using WebApplication1.Models;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

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
		public ActionResult Login([FromBody] UserLogin user)
		{
			if (user.Username == "admin" && user.Password == "1234")
			{
				string token = GenerateJwtToken(user.Username);
				return Ok(new { token });
			}

			return Unauthorized();
		}

		[HttpGet]
		[Authorize]
		public ActionResult GetValue()
		{
			Console.WriteLine("User successefully authorized");
			return Ok();
		}
		private string GenerateJwtToken(string username)
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
