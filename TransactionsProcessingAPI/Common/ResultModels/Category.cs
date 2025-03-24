namespace TransactionsProcessingAPI.Common.ResultModels;

public class Category(
    string categoryType,
    int transactionsCount)
{
    public string CategoryType { get; set; } = categoryType;
    public int TransactionsCount { get; set; } = transactionsCount;
}
