using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using JwtTokenSample.Models;

namespace JwtTokenSample.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ProductsController : ControllerBase
	{
		private readonly NorthwindContext _context;

		public ProductsController(NorthwindContext context)
		{
			_context = context;
		}

		[HttpGet("{id}", Name = "GetProduct")]
		public async Task<ActionResult<Products>> Get(int id)
		{
			var product = await _context.Products.FirstOrDefaultAsync(prd => prd.ProductID == id);
			if(product  == null) 
				return NotFound();

			return Ok(product);
		}

		[HttpPost]
		[Authorize(Roles = "admin")]
		public async Task<ActionResult<Products>> Post([FromBody] Products product)
		{
			await _context.Products.AddAsync(product);
			await _context.SaveChangesAsync();
			return Ok();
		}

		[HttpPut("{id}")]
		[Authorize(Roles = "admin")]
		public async Task<ActionResult<Products>> Put([FromBody] Products product)
		{
			if(product == null)
				return BadRequest();

			_context.Entry(product).State = EntityState.Modified;
			await _context.SaveChangesAsync();

			return Ok(product);
		}

		[HttpDelete("{id}")]
		[Authorize(Roles = "admin")]
		public async Task<ActionResult> Delete(int id)
		{
			var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductID == id);
			if( product == null)
				return NotFound();
			//_context.Products.Remove(product);
			_context.Entry(product).State = EntityState.Deleted;
			await _context.SaveChangesAsync();
			return Ok();
		}
	}
}
