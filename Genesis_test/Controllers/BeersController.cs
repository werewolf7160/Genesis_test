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
    public class BeersController : ControllerBase
    {
        private readonly AppDataContext _context;

        public BeersController(AppDataContext context)
        {
            _context = context;
        }

        // GET: api/Beers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Beer>>> GetBeers()
        {
            return await _context.Beers.ToListAsync();
        }

        // GET: api/Beers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Beer>> GetBeer(int id)
        {
            var beer = await _context.Beers.FindAsync(id);

            if (beer == null)
            {
                return NotFound();
            }

            return beer;
        }

        // PUT: api/Beers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBeer(int id, Beer beer)
        {
            if (id != beer.Id)
            {
                return BadRequest();
            }

            _context.ChangeTracker.Clear();
            _context.Entry(beer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BeerExists(id))
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

        // POST: api/Beers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Beer>> PostBeer(Beer beer)
        {
            _context.Beers.Add(beer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBeer", new { id = beer.Id }, beer);
        }

        // DELETE: api/Beers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBeer(int id)
        {
            var beer = await _context.Beers.FindAsync(id);
            if (beer == null)
            {
                return NotFound();
            }

            _context.Beers.Remove(beer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BeerExists(int id)
        {
            return _context.Beers.Any(e => e.Id == id);
        }


        // GET: api/Beers
        [HttpGet("/api/Beers/ByBrewerAndWholesaler")]
        public async Task<ActionResult<List<object>>> GetBeersByBrewerAndWholesaler()
        {
            var list = new List<object>();

            foreach (var beer in await _context.Beers.ToListAsync())
            {
                var stock = await _context.Stocks.Where(s => s.BeerId == beer.Id).Select(x=> x.WholesalerId).ToListAsync();
                list.Add(new {
                    beer = beer,
                    brewer = await _context.Brewers.FindAsync(beer.BrewerId),
                    wholesalers = await _context.Wholesalers.Where(x => stock.Contains(x.Id)).ToListAsync()
                });
            }

            return list;
        }

        // GET: api/Beers/5
        [HttpGet("{id}/ByBrewerAndWholesaler")]
        public async Task<ActionResult<object>> GetBeerByBrewerAndWholesaler(int id)
        {
            var beer = await _context.Beers.FindAsync(id);

            if (beer == null)
            {
                return NotFound();
            }
            var stock = await _context.Stocks.Where(s => s.BeerId == beer.Id).Select(x => x.WholesalerId).ToListAsync();
            dynamic res = new
            {
                beer = beer,
                brewer = await _context.Brewers.FindAsync(beer.BrewerId),
                wholesalers = await _context.Wholesalers.Where(x => stock.Contains(x.Id)).ToListAsync()
            };

            return res;
        }

    }
}
