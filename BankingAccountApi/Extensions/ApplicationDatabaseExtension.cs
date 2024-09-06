using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BankingAccountApi.Extensions;

public static class ApplicationDatabaseExtension
{
    public static void AddApplicationDatabase(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDatabaseContext>(options =>
        {
            
        });
        
        
    }
    
}