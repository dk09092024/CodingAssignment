using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ApplicationDatabaseContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<TransactionProtokol> TransactionProtokols { get; set; }
    
    public ApplicationDatabaseContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=banking.db");
    }
    
    
}