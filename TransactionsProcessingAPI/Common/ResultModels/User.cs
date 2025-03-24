namespace TransactionsProcessingAPI.Common.ResultModels;

public class User(
    Guid userId,
    decimal totalIncome,
    decimal totalExpense)
{
    public Guid UserId { get; set; } = userId;
    public decimal TotalIncome {  get; set; } = totalIncome;
    public decimal TotalExpense { get; set; } = totalExpense;
}
