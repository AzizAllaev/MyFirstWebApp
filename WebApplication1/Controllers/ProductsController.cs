using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using WebApplication1.Models;

namespace WebApplication1.Controllers
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
		public async Task<ActionResult<Products>> Post([FromBody] Products product)
		{
			await _context.Products.AddAsync(product);
			await _context.SaveChangesAsync();
			return Ok();
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<Products>> Put([FromBody] Products product, int id)
		{
			if(product == null) 
				return NotFound();

			_context.Entry(product).State = EntityState.Modified;
			await _context.SaveChangesAsync();

			return Ok();
		}

		[HttpDelete("{id}")]
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
