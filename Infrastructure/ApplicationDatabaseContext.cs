using System.Reflection;
using Domain.Model;
using Infrastructure.ModelConfiguration;
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
    public ApplicationDatabaseContext() : base()
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=../Infrastructure/Database/banking.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}