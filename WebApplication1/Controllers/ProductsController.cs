using Microsoft.AspNetCore.Mvc;
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
		public ActionResult<Products> Get(int id)
		{
			var product = _context.Products.FirstOrDefault(prd => prd.ProductID == id);
			if(product  == null) 
				return NotFound();

			return Ok(product);
		}

		[HttpPost("{id}")]
		public void Post()
		{

		}
	}
}
