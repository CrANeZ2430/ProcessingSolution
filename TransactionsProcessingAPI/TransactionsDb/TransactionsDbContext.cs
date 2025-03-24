using Microsoft.EntityFrameworkCore;
using TransactionsProcessingAPI.Common;

namespace TransactionsProcessingAPI.TransactionsDb;

public class TransactionsDbContext(
    DbContextOptions<TransactionsDbContext> options) 
    : DbContext(options)
{
    public static string TransactionsDbSchema = "transactions";
    public static string TransactionsMigrationHistory = "TransactionsDbMigrations";


    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("transactions");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TransactionsDbContext).Assembly);
    }
}
