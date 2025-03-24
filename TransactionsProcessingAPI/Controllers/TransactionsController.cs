using TransactionsProcessingAPI.Common;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TransactionsProcessingAPI.TransactionsDb;

namespace TransactionsProcessingAPI.Controllers;

[Route("api/transactions")]
public class TransactionsController(TransactionsDbContext dbContext) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetResult()
    {
        var result = await CsvReader.GenerateResultAsync("transactions_10_thousand.csv", dbContext);
        //var options = new JsonSerializerOptions
        //{
        //    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        //    WriteIndented = true
        //};
        //string serializedResult = JsonSerializer.Serialize(result, options);

        return Ok(result);
    }
}
