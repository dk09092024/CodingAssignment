using System.Reflection;
using MediatR;
using Domain;
using Domain.Features.Customer.AddCustomer;

namespace BankingAccountApi.Extensions;

public static class MediatorExtension
{
    public static void AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(AddCustomerHandler).Assembly));
        
    }
}