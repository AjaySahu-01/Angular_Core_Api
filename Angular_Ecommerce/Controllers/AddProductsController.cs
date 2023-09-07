using Angular_Ecommerce.Context;
using Angular_Ecommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace Angular_Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddProductsController : ControllerBase
    {
        private readonly UserDbContext userDbContext;

        public AddProductsController(UserDbContext userDbContext)
        {
            this.userDbContext = userDbContext;
        }
        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var cards = await userDbContext.products.ToListAsync();
            return Ok(cards);
        }


        [HttpGet]

        [Route("id:guid")]
        [ActionName("GetProduct")]

        public async Task<IActionResult> GetProduct([FromRoute] Guid id)
        {

            var card = await userDbContext.products.FirstOrDefaultAsync(x => x.Id == id);
            if (card != null)
            { return Ok(card); }
            return NotFound("Product not Found!");
        }
        [HttpPost("Addproduct")]
        public async Task<IActionResult> AddProduct([FromBody] Product products)
        {
            products.Id = Guid.NewGuid();
            await userDbContext.products.AddAsync(products);
            await userDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), products.Id, products);
        }

       

        [HttpDelete]
        [Route("id:guid")]
        public async Task<IActionResult> DeleteProduct([FromBody] Guid id)

        {
            var existingproduct = await userDbContext.products.FirstOrDefaultAsync(x => x.Id == id);
            if (existingproduct != null)
            {
                userDbContext.Remove(existingproduct);
                await userDbContext.SaveChangesAsync();
                return Ok(existingproduct);
            }
            return NotFound("Product Not Found!");
        }
    }
}
