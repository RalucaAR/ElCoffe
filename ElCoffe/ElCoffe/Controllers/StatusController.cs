using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElCoffe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace ElCoffe.Controllers
{
    public class StatusController : Controller
    {
        private DbConn db = new DbConn();

    // GET: api/Todo
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Status>>> GetAll()
    {
        return await db.Statuses.ToListAsync();
    }

    // GET: api/Todo/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Status>> GetStatus(long id)
    {
        var sts = await db.Statuses.FindAsync(id);

        if (sts == null)
        {
            return NotFound();
        }

        return sts;
    }

    [HttpPost]
    public async Task<ActionResult<Status>> Create([FromBody]Status sts)
    {
        db.Statuses.Add(sts);
        await db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetStatus), new { id = sts.Id }, sts);
    }

        // PUT: api/Todo/5
        [HttpPut]
        public async Task<IActionResult> UpdateReservation(Status sts)
        {
            db.Entry(sts).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStatus(long id)
    {
        var sts = await db.Statuses.FindAsync(id);

        if (sts == null)
        {
            return NotFound();
        }

        db.Statuses.Remove(sts);
        await db.SaveChangesAsync();

        return NoContent();
    }
}
}
