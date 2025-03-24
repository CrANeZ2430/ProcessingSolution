using TransactionsProcessingAPI.Common;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TransactionsProcessingAPI.TransactionsDb;
using Microsoft.EntityFrameworkCore;

namespace TransactionsProcessingAPI.Controllers;

[Route("api/transactions")]
public class TransactionsController(TransactionsDbContext dbContext) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetResult()
    {
        var result = await CsvReader.GenerateResultAsync("transactions_10_thousand.csv", dbContext);
        return Ok(result);
    }

    //For test purposes
    [HttpDelete]
    public IActionResult ClearDb()
    {
        dbContext.Transactions.ExecuteDelete();
        return Ok();
    }
}
