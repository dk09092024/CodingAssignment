using Domain.Repositories;
using Infrastructure.Repositories;

namespace TransactionApi.Extensions;

public static class RepositoryExtension
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>(); 
        services.AddScoped<ICustomerRepository, CustomerRepository>();
    }
}