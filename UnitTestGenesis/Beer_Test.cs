using Genesis_test.Controllers;
using Genesis_test.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace UnitTestGenesis;

public class Beer_Test: UnitTestBase
{

    [Fact]
    public void GetAll()
    {
        var context = GetPopulateDataContext();

        var controller = new BeersController(context);

        var beers = controller.GetBeers();

        var returnValue = Assert.IsType<ActionResult<IEnumerable<Beer>>>(beers.Result);
        var value = Assert.IsType<List<Beer>>(returnValue.Value);

        Assert.Equal(NbBeer, value.Count);
    }

    [Fact]
    public void GetBeer()
    {
        var context = GetPopulateDataContext();

        var controller = new BeersController(context);

        var beer = controller.GetBeer(1);

        var returnValue = Assert.IsType<ActionResult<Beer>>(beer.Result);
        var value = Assert.IsType<Beer>(returnValue.Value);

        Assert.Equal(1, value.Id);
    }

    [Fact]
    public void PutBeer()
    {
        var context = GetPopulateDataContext();

        var controller = new BeersController(context);

        var beer = new Beer
        {
            Id = 1,
            Name = "Beer1_updated",
            BrewerId = 1,
            Degree = 5.2,
            Price = 1.75
        };

        var result = controller.PutBeer(1, beer);

        Assert.IsType<NoContentResult>(result.Result);

        var beerUpdated = context.Beers.Find(1);

        Assert.Equal(beer.Name, beerUpdated.Name);
    }

    [Fact]
    //PutTest with different id => attended result is BadRequest
    public void PutBeerBadId()
    {
        var context = GetPopulateDataContext();

        var controller = new BeersController(context);

        var beer = new Beer
        {
            Id = 1,
            Name = "Beer1",
            BrewerId = 1,
            Degree = 5.2,
            Price = 1.75
        };

        var result = controller.PutBeer(2, beer);

        Assert.IsType<BadRequestResult>(result.Result);
    }

    [Fact]
    public  void PostBeer()
    {
        var context = GetPopulateDataContext();

        var controller = new BeersController(context);

        var beer = new Beer
        {
            Name = "Beer_posted",
            BrewerId = 1,
            Degree = 5.2,
            Price = 1.75
        };

        var result = controller.PostBeer(beer);
        var actionResult = Assert.IsType<ActionResult<Beer>>(result.Result);
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
        var returnValue = Assert.IsType<Beer>(createdAtActionResult.Value);

        Assert.Equal(beer.Name, returnValue.Name);


    }

    [Fact]
    //PutTest with unknown id => attended result is NotFound
    public void PutBeerNoExistingId()
    {
        var context = GetPopulateDataContext();

        var controller = new BeersController(context);

        var beer = new Beer
        {
            Id = 999,
            Name = "Beer1",
            BrewerId = 1,
            Degree = 5.2,
            Price = 1.75
        };

        var result = controller.PutBeer(999, beer);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public void DeleteBeer()
    {
        var context = GetPopulateDataContext();

        var controller = new BeersController(context);

        var result = controller.DeleteBeer(1);

        Assert.IsType<NoContentResult>(result.Result);

        var beer = context.Beers.Find(1);

        Assert.Null(beer);
    }
}