using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Genesis_test.Model;

namespace Genesis_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDataContext _context;

        public OrdersController(AppDataContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.Include(x => x.Lines).ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders.Include(x => x.Lines).FirstOrDefaultAsync(x => x.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }
            _context.ChangeTracker.Clear();
            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {

            if(order.Lines.Count <= 0)
                return BadRequest("Order must have at least one item");

            var wholesaler = await _context.Wholesalers.FindAsync(order.WholesalerId);
            //check if order Wholesaler exists
            if (wholesaler == null)
                return BadRequest($"Wholesaler {order.WholesalerId} does not exist");

            //check for duplicate in order lines
            if(order.Lines.GroupBy(x => x.BeerId).Where(x => x.Count() > 1).Select(x => x.Key).Any())
                return BadRequest("Duplicate items in order");

            //set reduction
            var reduction = 0.0;
            if(order.Lines.Count >= 10)
                reduction = 0.1;
            else if(order.Lines.Count >= 20)
                reduction = 0.2;

            foreach (var line in order.Lines)
            {
                //check minimum quantity
                if(line.Quantity <= 0)
                    return BadRequest("Quantity must be greater than 0");

                //get stock
                var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.BeerId == line.BeerId && x.WholesalerId == order.WholesalerId);

                //check if beer exists in stock
                if(stock == null)
                    return BadRequest($"Beer not found in stock of wholesaler {wholesaler.Name}");

                //check if enough stock
                if(stock.Nb < line.Quantity)
                    return BadRequest($"Not enough stock for beer {line.BeerId} ( max : {stock.Nb} )");

                //update TotalHt on line
                var beer = await _context.Beers.FindAsync(line.BeerId);
                line.TotalHt = beer.Price * line.Quantity * (1 - reduction);
            }

            //update global TotalHt
            order.TotalHt = order.Lines.Sum(x => x.TotalHt);

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            //decrease stock
            foreach (var line in order.Lines)
            {
                var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.BeerId == line.BeerId && x.WholesalerId == order.WholesalerId);
                stock.Nb -= line.Quantity;
                await _context.SaveChangesAsync();
            }

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
