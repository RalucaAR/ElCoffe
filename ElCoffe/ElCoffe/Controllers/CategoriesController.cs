﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElCoffe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace ElCoffe.Controllers
{

    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        private DbConn db = new DbConn();


        [HttpPost("Verificare")]
        public Category Verificare([FromBody]Category categ)
        {
            Category _categ = db.Categories
                      .Where(s => s.Id == categ.Id && s.Name == categ.Name)
                      .FirstOrDefault<Category>();
            return _categ;
        }



        // GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAll()
        {
            return await db.Categories.ToListAsync();
        }

        // GET: api/Todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(long id)
        {
            var categ = await db.Categories.FindAsync(id);

            if (categ == null)
            {
                return NotFound();
            }

            return categ;
        }

        [HttpPost]
        public async Task<ActionResult<Category>> Create([FromBody]Category categ)
        {
            db.Categories.Add(categ);
            await db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategory), new { id = categ.Id }, categ);
        }

        // PUT: api/Todo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(long id, Category categ)
        {
            if (id != categ.Id)
            {
                return BadRequest();
            }

            db.Entry(categ).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(long id)
        {
            var categ = await db.Categories.FindAsync(id);

            if (categ == null)
            {
                return NotFound();
            }

            db.Categories.Remove(categ);
            await db.SaveChangesAsync();

            return NoContent();
        }
    }
}