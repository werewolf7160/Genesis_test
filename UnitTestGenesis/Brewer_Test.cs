using Genesis_test.Controllers;
using Genesis_test.Model;
using Microsoft.AspNetCore.Mvc;

namespace UnitTestGenesis;

public class Brewer_Test : UnitTestBase
{

   //create test as in Beer_Test.cs
   [Fact]
    public void GetAll()
    {
        var context = GetPopulateDataContext();

        var controller = new BrewersController(context);

        var brewers = controller.GetBrewers();

        var returnValue = Assert.IsType<ActionResult<IEnumerable<Brewer>>>(brewers.Result);
        var value = Assert.IsType<List<Brewer>>(returnValue.Value);

        Assert.Equal(NbBrewers, value.Count);
    }

    [Fact]
    public void GetBrewer()
    {
        var context = GetPopulateDataContext();

        var controller = new BrewersController(context);

        var brewer = controller.GetBrewer(1);

        var returnValue = Assert.IsType<ActionResult<Brewer>>(brewer.Result);
        var value = Assert.IsType<Brewer>(returnValue.Value);

        Assert.Equal(1, value.Id);
    }

    [Fact]
    public void GetBrewerBeers()
    {
        var context = GetPopulateDataContext();

        var controller = new BrewersController(context);

        var beers = controller.GetBrewerBeers(1);

        var returnValue = Assert.IsType<ActionResult<IEnumerable<Beer>>>(beers.Result);
        var value = Assert.IsType<List<Beer>>(returnValue.Value);

        var nb = context.Beers.Where(b => b.BrewerId == 1).Count();

        Assert.Equal(nb, value.Count);
    }


    [Fact]
    public void PostBrewer()
    {
        var context = GetPopulateDataContext();

        var controller = new BrewersController(context);

        var brewer = new Brewer()
        {
            Name = "Brewer_posted",
        };

        var result = controller.PostBrewer(brewer);
        var actionResult = Assert.IsType<ActionResult<Brewer>>(result.Result);
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
        var returnValue = Assert.IsType<Brewer>(createdAtActionResult.Value);

        Assert.Equal(brewer.Name, returnValue.Name);
    }

    [Fact]
    public void PutBrewer()
    {
        var context = GetPopulateDataContext();

        var controller = new BrewersController(context);

        var brewer = new Brewer
        {
            Id = 1,
            Name = "Brewer1_updated",
        };

        var result = controller.PutBrewer(1, brewer);

        Assert.IsType<NoContentResult>(result.Result);

        var brewerUpdated = context.Brewers.Find(1);

        Assert.Equal(brewer.Name, brewerUpdated.Name);
    }

    [Fact]
    //PutTest with different id => attended result is BadRequest
    public void PutBrewerBadId()
    {
        var context = GetPopulateDataContext();

        var controller = new BrewersController(context);

        var brewer = new Brewer
        {
            Id = 1,
            Name = "Brewer1_updated",
        };

        var result = controller.PutBrewer(2, brewer);

        Assert.IsType<BadRequestResult>(result.Result);
    }

    [Fact]
    //PutTest with unknown id => attended result is NotFound
    public void PutBrewerNoExistingId()
    {
        var context = GetPopulateDataContext();

        var controller = new BrewersController(context);

        var brewer = new Brewer
        {
            Id = 999,
            Name = "Brewer1_updated",
        };

        var result = controller.PutBrewer(999, brewer);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public void DeleteBrewer()
    {
        var context = GetPopulateDataContext();

        var controller = new BrewersController(context);

        var result = controller.DeleteBrewer(1);

        Assert.IsType<NoContentResult>(result.Result);

        var brewer = context.Brewers.Find(1);

        Assert.Null(brewer);
    }
}