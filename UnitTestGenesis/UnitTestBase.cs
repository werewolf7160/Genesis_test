using Genesis_test.Model;
using Microsoft.EntityFrameworkCore;

namespace UnitTestGenesis;

public class UnitTestBase
{

    private List<Brewer> _brewers = new List<Brewer>
    {
        new(){Id=1,Name = "Brewer1"},
        new(){Id=2,Name = "Brewer2"},
    };

    //create list of Wholesaler
    private List<Wholesaler> _wholesalers = new List<Wholesaler>
    {
        new(){Id=1,Name = "Wholesaler1"},
        new(){Id=2,Name = "Wholesaler2"},
    };

    //create list of Stock
    private List<Stock> _stocks = new List<Stock>
    {
        new(){Id=1,WholesalerId = 1, BeerId = 1, Nb = 100},
        new(){Id=2,WholesalerId = 1, BeerId = 2, Nb = 200},
        new(){Id=3,WholesalerId = 2, BeerId = 1, Nb = 300},
        new(){Id=4,WholesalerId = 2, BeerId = 2, Nb = 300},
    };

    private List<Beer> _beers = new()
    {
        new(){Id=1,Name = "Beer1", BrewerId = 1, Degree = 5.2, Price = 1.75},
        new(){Id=2,Name = "Beer2", BrewerId = 1, Degree = 4.2, Price = 2.75},
        new(){Id=3,Name = "Beer3", BrewerId = 2, Degree = 6.2, Price = 3.75},
    };

    private List<Order> _orders = new()
    {
        new (){Id = 1,Lines = new ()
        {
            new (){ Id = 1, BeerId = 1, Quantity = 1, TotalHt = 1.75},
            new (){ Id = 2, BeerId = 2, Quantity = 25, TotalHt = 2.75},
        },TotalHt = 70.50, WholesalerId = 1},
        new (){Id = 2,Lines = new ()
        {
            new (){ Id = 3, BeerId = 1, Quantity = 1, TotalHt = 1.75},
            new (){ Id = 4, BeerId = 2, Quantity = 25, TotalHt = 2.75},
            new (){ Id = 5, BeerId = 3, Quantity = 10, TotalHt = 3.75},
        },TotalHt = 108, WholesalerId = 2}
    };

    protected int NbBeer => _beers.Count;
    protected int NbStock => _stocks.Count;
    protected int NbWholesalers => _wholesalers.Count;
    protected int NbBrewers => _brewers.Count;
    protected int NbOrders => _orders.Count;

    private AppDataContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDataContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;

        var context = new AppDataContext(options);
        context.Database.OpenConnection();
        context.Database.EnsureCreated();

        return context;
    }

    private async Task PopulateInMemoryDataContext(AppDataContext dataContext)
    {
        dataContext.AddRange(_brewers);
        dataContext.AddRange(_beers);
        dataContext.AddRange(_wholesalers);
        dataContext.AddRange(_stocks);
        dataContext.AddRange(_orders);

        await dataContext.SaveChangesAsync();
    }


    protected AppDataContext GetPopulateDataContext()
    {
        var context = GetInMemoryDbContext();
        PopulateInMemoryDataContext(context);
        return context;
    }

}