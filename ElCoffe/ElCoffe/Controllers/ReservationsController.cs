using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElCoffe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace ElCoffe.Controllers
{
    public class ReservationsController : Controller
    {
        private DbConn db = new DbConn();

        // GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetAll()
        {
            return await db.Reservations.ToListAsync();
        }

        // GET: api/Todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> GetReservation(long id)
        {
            var rez = await db.Reservations.FindAsync(id);

            if (rez == null)
            {
                return NotFound();
            }

            return rez;
        }

        [HttpPost]
        public async Task<ActionResult<Reservation>> Create([FromBody]Reservation rez)
        {
            db.Reservations.Add(rez);
            await db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReservation), new { id = rez.Id }, rez);
        }

        // PUT: api/Todo/5
        [HttpPut]
        public async Task<IActionResult> UpdateReservation(Reservation rez)
        {
            db.Entry(rez).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(long id)
        {
            var rez = await db.Reservations.FindAsync(id);

            if (rez == null)
            {
                return NotFound();
            }

            db.Reservations.Remove(rez);
            await db.SaveChangesAsync();

            return NoContent();
        }
    }
}
