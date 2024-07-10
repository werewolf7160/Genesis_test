﻿using Genesis_test.Controllers;
using Genesis_test.Model;
using Microsoft.AspNetCore.Mvc;

namespace UnitTestGenesis;

public class Stock_Test : UnitTestBase
{

    [Fact]
    public void GetAll()
    {
        var context = GetPopulateDataContext();

        var controller = new StocksController(context);

        var stocks = controller.GetStocks();

        var returnValue = Assert.IsType<ActionResult<IEnumerable<Stock>>>(stocks.Result);
        var value = Assert.IsType<List<Stock>>(returnValue.Value);

        Assert.Equal(NbStock, value.Count);
    }

    [Fact]
    public void GetStock()
    {
        var context = GetPopulateDataContext();

        var controller = new StocksController(context);

        var stock = controller.GetStock(1);

        var returnValue = Assert.IsType<ActionResult<Stock>>(stock.Result);
        var value = Assert.IsType<Stock>(returnValue.Value);

        Assert.Equal(1, value.Id);
    }

    [Fact]
    public void PutStock()
    {
        var context = GetPopulateDataContext();

        var controller = new StocksController(context);

        var stock = new Stock
        {
            Id = 1,
            WholesalerId = 1,
            BeerId = 1,
            Nb = 200
        };

        var result = controller.PutStock(1, stock);

        Assert.IsType<NoContentResult>(result.Result);

        var stockUpdated = context.Stocks.Find(1);

        Assert.Equal(stock.Nb, stockUpdated.Nb);
    }

    [Fact]
    public void PutStockBadId()
    {
       var context = GetPopulateDataContext();

       var controller = new StocksController(context);

       var stock = new Stock
       {
           Id = 1,
           WholesalerId = 1,
           BeerId = 1,
           Nb = 200
       };

       var result = controller.PutStock(2, stock);

       Assert.IsType<BadRequestResult>(result.Result);
    }

    [Fact]
    public void PutStockNoExistingId()
    {
       var context = GetPopulateDataContext();

       var controller = new StocksController(context);

       var stock = new Stock
       {
           Id = 999,
           WholesalerId = 1,
           BeerId = 1,
           Nb = 200
       };

       var result = controller.PutStock(999, stock);

       Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public void DeleteStock()
    {
        var context = GetPopulateDataContext();

        var controller = new StocksController(context);

        var result = controller.DeleteStock(1);

        Assert.IsType<NoContentResult>(result.Result);

        var stock = context.Stocks.Find(1);

        Assert.Null(stock);
    }
}