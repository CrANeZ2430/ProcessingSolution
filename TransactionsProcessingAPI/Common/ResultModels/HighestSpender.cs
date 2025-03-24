namespace TransactionsProcessingAPI.Common.ResultModels;

public record HighestSpender(
    Guid UserId,
    decimal TotalExpenses);
