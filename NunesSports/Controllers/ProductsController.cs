using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NunesSports.Data;
using NunesSports.Shared.Entities;

namespace NunesSports.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DataContext _context;

        public  ProductsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductByIdAsync(int id)
        {
            var result = await _context.Products.FindAsync(id);
            if (result == null)
                return NotFound("Produto Não Encontrado");
            
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            var result = await _context.Products.FindAsync(id);
            if (result == null)
                return NotFound("Produto Não Encontrado");

            _context.Products.Remove(result);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> UpdateProductAsync(int id, Product updatedProduct)
        {
            var dbProduct = await _context.Products.FindAsync(id);
            if (dbProduct == null)
                return NotFound("Produto Não Encontrado");

            dbProduct.Name = updatedProduct.Name;
            dbProduct.Description = updatedProduct.Description;
            dbProduct.Price = updatedProduct.Price;

            await _context.SaveChangesAsync();

            return Ok(dbProduct);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProductAsync(Product newProduct)
        {
            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();

            return Ok(newProduct);
        }
    }
}
