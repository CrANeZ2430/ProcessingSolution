namespace TransactionsProcessingAPI.Common;

public record Transaction(
    Guid TransactionId, 
    Guid UserId, 
    DateTime Date, 
    decimal Amount, 
    string Category, 
    string Description, 
    string Merchant);
