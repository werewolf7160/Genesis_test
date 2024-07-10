using Microsoft.EntityFrameworkCore;

namespace Genesis_test.Model;

public class AppDataContext : DbContext
{

    #region Variable

    #endregion

    #region Property

    public DbSet<Beer> Beers { get; set; }
    public DbSet<Wholesaler> Wholesalers { get; set; }
    public DbSet<Brewer> Brewers { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Order> Orders{ get; set; }

    #endregion

    #region Constructor
    public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
    {

    }
    #endregion

    #region Method

    #endregion

    #region Override

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    #endregion


}