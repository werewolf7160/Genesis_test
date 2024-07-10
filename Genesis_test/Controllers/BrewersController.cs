using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Genesis_test.Model;

namespace Genesis_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrewersController : ControllerBase
    {
        private readonly AppDataContext _context;

        public BrewersController(AppDataContext context)
        {
            _context = context;
        }

        // GET: api/Brewers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brewer>>> GetBrewers()
        {
            return await _context.Brewers.ToListAsync();
        }

        // GET: api/Brewers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Brewer>> GetBrewer(int id)
        {
            var brewer = await _context.Brewers.FindAsync(id);

            if (brewer == null)
            {
                return NotFound();
            }

            return brewer;
        }

        // GET: api/Brewers/5/beers
        [HttpGet("{id}/beers")]
        public async Task<ActionResult<IEnumerable<Beer>>> GetBrewerBeers(int id)
        {
            var beers = await _context.Beers.Where(b => b.BrewerId == id).ToListAsync();

            return beers.Any() ? beers : NotFound();
        }

        // PUT: api/Brewers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrewer(int id, Brewer brewer)
        {
            if (id != brewer.Id)
            {
                return BadRequest();
            }
            _context.ChangeTracker.Clear();
            _context.Entry(brewer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrewerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Brewers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Brewer>> PostBrewer(Brewer brewer)
        {
            _context.Brewers.Add(brewer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBrewer", new { id = brewer.Id }, brewer);
        }

        // DELETE: api/Brewers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrewer(int id)
        {
            var brewer = await _context.Brewers.FindAsync(id);
            if (brewer == null)
            {
                return NotFound();
            }

            _context.Brewers.Remove(brewer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BrewerExists(int id)
        {
            return _context.Brewers.Any(e => e.Id == id);
        }
    }
}
