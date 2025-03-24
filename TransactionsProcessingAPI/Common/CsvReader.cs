using TransactionsProcessingAPI.Common.ResultModels;
using TransactionsProcessingAPI.TransactionsDb;

namespace TransactionsProcessingAPI.Common;

public static class CsvReader
{
    private static async IAsyncEnumerable<Transaction> ParseLog(
        string filePath, 
        SemaphoreSlim semaphoreSlim)
    {
        using StreamReader reader = new StreamReader(filePath);
        while (!reader.EndOfStream)
        {
            await semaphoreSlim.WaitAsync();
            var line = await reader.ReadLineAsync();
            semaphoreSlim.Release();
            if (line.TryParse(out Transaction model))
                yield return model;
        }
    }

    public static async Task<Result> GenerateResultAsync(
        string filePath, 
        TransactionsDbContext dbContext)
    {
        var users = new Dictionary<Guid, User>();
        var categories = new Dictionary<string, Category>();
        var transactions = new List<Transaction>();

        await foreach (var transaction in ParseLog(filePath, new SemaphoreSlim(1)))
        {
            if (!users.TryGetValue(transaction.UserId, out var user))
            {
                user = transaction.Amount <= 0
                    ? new User(transaction.UserId, 0, transaction.Amount)
                    : new User(transaction.UserId, transaction.Amount, 0);
                users.Add(transaction.UserId, user);
            }
            else
            {
                if (transaction.Amount <= 0)
                    user.TotalExpense += transaction.Amount;
                else
                    user.TotalIncome += transaction.Amount;
            }

            if (!categories.TryGetValue(transaction.Category, out var category))
                categories[transaction.Category] = new Category(transaction.Category, 1);
            else
                category.TransactionsCount++;
        }

        await dbContext.AddRangeAsync(transactions);
        await dbContext.SaveChangesAsync();

        var topCategories = categories.Values
            .OrderByDescending(x => x.TransactionsCount)
            .Take(3);
        var highestSpender = users.Values
            .OrderByDescending(x => x.TotalExpense)
            .Select(x => new HighestSpender(x.UserId, x.TotalExpense))
            .FirstOrDefault();

        var result = new Result(
            users.Values,
            topCategories,
            highestSpender!);

        return result;
    }
}
