namespace TransactionsProcessingAPI.Common.ResultModels;

public record Result(
    IEnumerable<User> UsersSummary,
    IEnumerable<Category> TopCategories,
    HighestSpender HighestSpender);
