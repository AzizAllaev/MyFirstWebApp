using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using JwtTokenSample.Models;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using JwtTokenSample.Service;

namespace JwtTokenSample.Controllers
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
				string token = AuthService.GenerateJwtToken(user.Username);
				return Ok(new { token });
			}
			
			return Unauthorized();
		}
	}
}
