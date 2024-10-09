using System.Reflection;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ApplicationDatabaseContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<TransactionProtocol> TransactionProtocols { get; set; }
    
    public ApplicationDatabaseContext(DbContextOptions options) : base(options)
    {
        
    }
    public ApplicationDatabaseContext()
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=/Database/banking.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}