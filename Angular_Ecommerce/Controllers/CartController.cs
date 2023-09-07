using Angular_Ecommerce.Context;
using Angular_Ecommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Angular_Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly UserDbContext userDbContext;

        public CartController(UserDbContext userDbContext)
        {
            this.userDbContext = userDbContext;
        }

        [HttpPost("add")]
        public ActionResult<Cart> AddToCart([FromBody] Cart cartItem)
        {
            if (cartItem == null)
            {
                return BadRequest("Invalid cart item data");
            }
            var existingCartItem = userDbContext.Carts.FirstOrDefault(c => c.Product == cartItem.Product);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += cartItem.Quantity;
            }
            else
            {
                userDbContext.Carts.Add(cartItem);
            }
            userDbContext.SaveChanges();

            if (existingCartItem != null)
            {
                return Ok(existingCartItem);
            }
            else
            {
  
                return CreatedAtAction(nameof(GetCartItem), new { id = cartItem.Id }, cartItem);
            }
        }
        [HttpGet("{id}")]
        public ActionResult<Cart> GetCartItem(int id)
        {
            var cartItem = userDbContext.Carts.FirstOrDefault(c => c.Id == id);

            if (cartItem == null)
            {
                return NotFound();
            }

            return Ok(cartItem);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteFromCart(int id)
        {
            var cartItem = userDbContext.Carts.Find(id);
            if (cartItem == null)
            {
                return NotFound();
            }

            userDbContext.Carts.Remove(cartItem);
            userDbContext.SaveChanges();
            return NoContent();
        }

        
        [HttpPut("update-quantity/{id}")]
        public IActionResult UpdateProductQuantity(int id, [FromBody] int newQuantity)
        {
            var cartItem = userDbContext.Carts.Find(id);
            if (cartItem == null)
            {
                return NotFound();
            }

            cartItem.Quantity = newQuantity;
            userDbContext.SaveChanges();
            return Ok(cartItem);
        }

    
        [HttpGet]
        public ActionResult<IEnumerable<Cart>> GetCart()
        {
            List<Cart> cart = userDbContext.Carts.ToList();
            return Ok(cart);
        }
    }
}
   

   
