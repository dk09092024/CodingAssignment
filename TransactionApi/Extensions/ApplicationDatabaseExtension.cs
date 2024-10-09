using Infrastructure;

namespace TransactionApi.Extensions;

public static class ApplicationDatabaseExtension
{
    public static void AddApplicationDatabase(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDatabaseContext>();
        
        
    }
    
}