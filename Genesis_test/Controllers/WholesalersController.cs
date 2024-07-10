using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Genesis_test.Model;

namespace Genesis_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WholesalersController : ControllerBase
    {
        private readonly AppDataContext _context;

        public WholesalersController(AppDataContext context)
        {
            _context = context;
        }

        // GET: api/Wholesalers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Wholesaler>>> GetWholesalers()
        {
            return await _context.Wholesalers.ToListAsync();
        }

        // GET: api/Wholesalers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Wholesaler>> GetWholesaler(int id)
        {
            var wholeSaler = await _context.Wholesalers.FindAsync(id);

            if (wholeSaler == null)
            {
                return NotFound();
            }

            return wholeSaler;
        }

        // GET: api/Wholesalers/5/stocks
        [HttpGet("{id}/stocks")]
        public async Task<ActionResult<IEnumerable<Stock>>> GetWholeSalerStock(int id)
        {
            var stocks = await _context.Stocks.Where(x => x.WholesalerId == id).ToListAsync();

            return stocks.Any() ? stocks : NotFound();
        }

        // PUT: api/Wholesalers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWholesaler(int id, Wholesaler wholesaler)
        {
            if (id != wholesaler.Id)
            {
                return BadRequest();
            }
            _context.ChangeTracker.Clear();
            _context.Entry(wholesaler).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WholeSalerExists(id))
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

        // POST: api/Wholesalers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Wholesaler>> PostWholeSaler(Wholesaler wholesaler)
        {
            _context.Wholesalers.Add(wholesaler);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWholesaler", new { id = wholesaler.Id }, wholesaler);
        }

        // DELETE: api/Wholesalers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWholesaler(int id)
        {
            var wholeSaler = await _context.Wholesalers.FindAsync(id);
            if (wholeSaler == null)
            {
                return NotFound();
            }

            _context.Wholesalers.Remove(wholeSaler);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WholeSalerExists(int id)
        {
            return _context.Wholesalers.Any(e => e.Id == id);
        }



    }
}
