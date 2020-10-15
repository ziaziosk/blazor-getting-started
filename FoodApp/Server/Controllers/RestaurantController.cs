using System.Threading.Tasks;
using FoodApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class RestaurantController : ControllerBase
{
    private readonly ApplicationDBContext _context;
    public RestaurantController(ApplicationDBContext context)
    {
        this._context = context;
    }

    [HttpGet]
        public async Task<IActionResult> Get()
        {
            var stores = await _context.Restaurants.ToListAsync();
            return Ok(stores);
        }


         [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var store = await _context.Restaurants.FirstOrDefaultAsync(a=>a.Id ==id);
            return Ok(store);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Restaurant restaurant)
        {
            _context.Add(restaurant);
            await _context.SaveChangesAsync();
            return Ok(restaurant.Id); 
        }

        [HttpPut]
        public async Task<IActionResult> Put(Restaurant restaurant)
        {
            _context.Entry(restaurant).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var r = new Restaurant { Id = id };
            _context.Remove(r);
            await _context.SaveChangesAsync();
            return NoContent();
        }
}