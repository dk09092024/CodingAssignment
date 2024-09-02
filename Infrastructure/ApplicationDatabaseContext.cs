using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ApplicationDatabaseContext : DbContext
{
    
    public ApplicationDatabaseContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
    }
    
    
}