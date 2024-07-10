using Genesis_test.Controllers;
using Genesis_test.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;

namespace UnitTestGenesis;

public class Order_Test : UnitTestBase
{
    [Fact]
    public void GetAll()
    {
        var context = GetPopulateDataContext();

        var controller = new OrdersController(context);

        var orders = controller.GetOrders();

        var returnValue = Assert.IsType<ActionResult<IEnumerable<Order>>>(orders.Result);
        var value = Assert.IsType<List<Order>>(returnValue.Value);

        Assert.Equal(NbOrders, value.Count);
    }

    [Fact]
    public void GetOrder()
    {
        var context = GetPopulateDataContext();

        var controller = new OrdersController(context);

        var order = controller.GetOrder(1);

        var returnValue = Assert.IsType<ActionResult<Order>>(order.Result);
        var value = Assert.IsType<Order>(returnValue.Value);

        Assert.Equal(1, value.Id);
        Assert.Equal(2, value.Lines.Count);
    }

    [Fact]
    public void PutOrder()
    {
        var context = GetPopulateDataContext();

        var controller = new OrdersController(context);

        var order = new Order
        {
            Id = 1,
            Lines = new()
            {
                new() { Id = 1, BeerId = 1, Quantity = 1, TotalHt = 1.5 },
                new() { Id = 2, BeerId = 2, Quantity = 25, TotalHt = 1.35 },
            },
            TotalHt = 35.25,
            WholesalerId = 2
        };

        var result = controller.PutOrder(1, order);

        Assert.IsType<NoContentResult>(result.Result);

        var orderUpdated = context.Orders.Find(1);

        Assert.Equal(order.WholesalerId, orderUpdated.WholesalerId);
    }

    [Fact]
    public void PutOrderBadId()
    {
        var context = GetPopulateDataContext();

        var controller = new OrdersController(context);

        var order = new Order
        {
            Id = 1,
            Lines = new()
            {
                new() { Id = 1, BeerId = 1, Quantity = 1, TotalHt = 1.5 },
                new() { Id = 2, BeerId = 2, Quantity = 25, TotalHt = 1.35 },
            },
            TotalHt = 35.25,
            WholesalerId = 2
        };

        var result = controller.PutOrder(2, order);

        Assert.IsType<BadRequestResult>(result.Result);
    }

    [Fact]
    public void PutOrderNonExistingId()
    {
        var context = GetPopulateDataContext();

        var controller = new OrdersController(context);

        var order = new Order
        {
            Id = 999,
            Lines = new()
            {
                new() { Id = 1, BeerId = 1, Quantity = 1, TotalHt = 1.5 },
                new() { Id = 2, BeerId = 2, Quantity = 25, TotalHt = 1.35 },
            },
            TotalHt = 35.25,
            WholesalerId = 2
        };

        var result = controller.PutOrder(999, order);

        Assert.IsType<NotFoundResult>(result.Result);
    }


    //check post with empty line
    [Fact]
    public void PostOrderEmptyLine()
    {
        var context = GetPopulateDataContext();

        var controller = new OrdersController(context);

        var order = new Order
        {
            Lines = new() { },
            TotalHt = 36.75,
            WholesalerId = 2
        };

        var result = controller.PostOrder(order);
        var r = result.Result;

        var actionResult = Assert.IsType<ActionResult<Order>>(result.Result);
        var res = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        Assert.Matches("Order must have at least one item", res.Value.ToString());
    }

    [Fact]
    //check if total is correctly calculated
    public void PostOrderCheckTotal()
    {
        var context = GetPopulateDataContext();

        var controller = new OrdersController(context);

        var order = new Order
        {
            Lines = new()
            {
                new() { BeerId = 1, Quantity = 1 },
                new() { BeerId = 2, Quantity = 1 },
            },
            WholesalerId = 2
        };

        var result = controller.PostOrder(order);

        var actionResult = Assert.IsType<ActionResult<Order>>(result.Result);
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
        var returnValue = Assert.IsType<Order>(createdAtActionResult.Value);

        Assert.Equal(4.5 , returnValue.TotalHt);
    }

    [Fact]
    //check existence for wholesaler
    public void PostOrderNonExistentWholesaler()
    {
        var context = GetPopulateDataContext();

        var controller = new OrdersController(context);

        var order = new Order
        {
            Lines = new()
            {
                new() { BeerId = 1, Quantity = 1 },
                new() { BeerId = 2, Quantity = 1 },
            },
            WholesalerId = 999
        };

        var result = controller.PostOrder(order);

        var actionResult = Assert.IsType<ActionResult<Order>>(result.Result);
        var res = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        Assert.Matches("does not exist", res.Value.ToString());

        
    }

    [Fact]
    //check duplicate
    public void PostOrderDuplicateBeer()
    {
        var context = GetPopulateDataContext();

        var controller = new OrdersController(context);

        var order = new Order
        {
            Lines = new()
            {
                new() { BeerId = 1, Quantity = 1 },
                new() { BeerId = 1, Quantity = 1 },
            },
            WholesalerId = 1
        };

        var result = controller.PostOrder(order);

        var actionResult = Assert.IsType<ActionResult<Order>>(result.Result);
        var res = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        Assert.Matches("Duplicate items in order", res.Value.ToString());
        
    }

    [Fact]
    //check quantity (too much) => expected badRequest
    public void PostOrderQuantity()
    {
        var context = GetPopulateDataContext();

        var controller = new OrdersController(context);

        var order = new Order
        {
            Lines = new()
            {
                new() { BeerId = 1, Quantity = 9999 },
            },
            WholesalerId = 1
        };

        var result = controller.PostOrder(order);

        var actionResult = Assert.IsType<ActionResult<Order>>(result.Result);
        var res = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        Assert.Matches("Not enough stock for beer", res.Value.ToString());
    }

    [Fact]
    //check quantity (wholesaler don't sell it)
    public void PostOrderNotInWholesalerStock()
    {
        var context = GetPopulateDataContext();

        var controller = new OrdersController(context);

        var order = new Order
        {
            Lines = new()
            {
                new() { BeerId = 3, Quantity = 10 },
            },
            WholesalerId = 1
        };

        var result = controller.PostOrder(order);

        var actionResult = Assert.IsType<ActionResult<Order>>(result.Result);
        var res = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        Assert.Matches("don't sale this beer", res.Value.ToString() );
    }


    [Fact]
    //check quantity (0 quantity on a line)
    public void PostOrderQuantityZero()
    {
        var context = GetPopulateDataContext();

        var controller = new OrdersController(context);

        var order = new Order
        {
            Lines = new()
            {
                new() { BeerId = 3, Quantity = 0 },
            },
            WholesalerId = 1
        };

        var result = controller.PostOrder(order);

        var actionResult = Assert.IsType<ActionResult<Order>>(result.Result);
        var res = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        Assert.Matches("Quantity must be greater than 0", res.Value.ToString());
    }

    [Fact]
    //check for 10%
    public void PostOrder10Percent()
    {
        var context = GetPopulateDataContext();

        var controller = new OrdersController(context);

        var order = new Order
        {
            Lines = new()
            {
                new() { BeerId = 1, Quantity = 10 },
            },
            WholesalerId = 1
        };

        var result = controller.PostOrder(order);

        var actionResult = Assert.IsType<ActionResult<Order>>(result.Result);
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
        var returnValue = Assert.IsType<Order>(createdAtActionResult.Value);

        Assert.Equal(15.75, returnValue.TotalHt);
    }

    [Fact]
    //check for 20%
    public void PostOrder20Percent()
    {
        var context = GetPopulateDataContext();

        var controller = new OrdersController(context);

        var order = new Order
        {
            Lines = new()
            {
                new() { BeerId = 1, Quantity = 20 },
            },
            WholesalerId = 1
        };

        var result = controller.PostOrder(order);

        var actionResult = Assert.IsType<ActionResult<Order>>(result.Result);
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
        var returnValue = Assert.IsType<Order>(createdAtActionResult.Value);

        Assert.Equal(28, returnValue.TotalHt);
    }
}