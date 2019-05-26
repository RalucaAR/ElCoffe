using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElCoffe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace ElCoffe.Controllers
{
    public class OrdersController : Controller
    {
        private DbConn db = new DbConn();

        // GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAll()
        {
            return await db.Orders.ToListAsync();
        }

        // GET: api/Todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(long id)
        {
            var ord = await db.Orders.FindAsync(id);

            if (ord == null)
            {
                return NotFound();
            }

            return ord;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> Create([FromBody]Order ord)
        {
            db.Orders.Add(ord);
            await db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new { id = ord.Id }, ord);
        }

        // PUT: api/Todo/5
        [HttpPut]
        public async Task<IActionResult> UpdateOrder(Order ord)
        {
            db.Entry(ord).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(long id)
        {
            var ord = await db.Orders.FindAsync(id);

            if (ord == null)
            {
                return NotFound();
            }

            db.Orders.Remove(ord);
            await db.SaveChangesAsync();

            return NoContent();
        }
    }
}
