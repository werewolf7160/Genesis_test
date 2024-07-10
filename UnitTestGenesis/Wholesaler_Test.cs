using Genesis_test.Controllers;
using Genesis_test.Model;
using Microsoft.AspNetCore.Mvc;

namespace UnitTestGenesis;

public class Wholesaler_Test : UnitTestBase
{

    [Fact]
    public void GetAll()
    {
        var context = GetPopulateDataContext();

        var controller = new WholesalersController(context);

        var wholesalers = controller.GetWholesalers();

        var returnValue = Assert.IsType<ActionResult<IEnumerable<Wholesaler>>>(wholesalers.Result);
        var value = Assert.IsType<List<Wholesaler>>(returnValue.Value);

        Assert.Equal(NbWholesalers, value.Count);
    }

    [Fact]
    public void GetWholesaler()
    {
        var context = GetPopulateDataContext();

        var controller = new WholesalersController(context);

        var wholesaler = controller.GetWholesaler(1);

        var returnValue = Assert.IsType<ActionResult<Wholesaler>>(wholesaler.Result);
        var value = Assert.IsType<Wholesaler>(returnValue.Value);

        Assert.Equal(1, value.Id);
    }

    [Fact]
    public void PostWholesaler()
    {
        var context = GetPopulateDataContext();

        var controller = new WholesalersController(context);

        var wholesaler = new Wholesaler() { Name = "Wholesaler3" };

        var result = controller.PostWholeSaler(wholesaler);
        var actionResult = Assert.IsType<ActionResult<Wholesaler>>(result.Result);
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
        var returnValue = Assert.IsType<Wholesaler>(createdAtActionResult.Value);

        Assert.Equal(wholesaler.Name, returnValue.Name);
    }


    [Fact]
    public void PutWholesaler()
    {
        var context = GetPopulateDataContext();

        var controller = new WholesalersController(context);

        var wholesaler = new Wholesaler
        {
            Id = 1,
            Name = "Wholesaler1_updated",
        };

        var result = controller.PutWholesaler(1, wholesaler);

        Assert.IsType<NoContentResult>(result.Result);

        var wholesalerUpdated = context.Wholesalers.Find(1);
        Assert.Equal(wholesaler.Name, wholesalerUpdated.Name);
    }

    [Fact]
    public void PutWholesalerBadId()
    {
        var context = GetPopulateDataContext();

        var controller = new WholesalersController(context);

        var wholesaler = new Wholesaler
        {
            Id = 1,
            Name = "Wholesaler1_updated",
        };

        var result = controller.PutWholesaler(2, wholesaler);

        Assert.IsType<BadRequestResult>(result.Result);
    }

    [Fact]
    public void PutWholesalerNoExistingId()
    {
        var context = GetPopulateDataContext();

        var controller = new WholesalersController(context);

        var wholesaler = new Wholesaler
        {
            Id = 999,
            Name = "Wholesaler1_updated",
        };

        var result = controller.PutWholesaler(999, wholesaler);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public void DeleteWholesaler()
    {
        var context = GetPopulateDataContext();

        var controller = new WholesalersController(context);

        var result = controller.DeleteWholesaler(1);

        Assert.IsType<NoContentResult>(result.Result);

        var wholesaler = context.Wholesalers.Find(1);

        Assert.Null(wholesaler);
    }
}