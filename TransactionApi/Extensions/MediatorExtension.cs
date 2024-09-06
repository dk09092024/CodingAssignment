using Domain.Features.Customer.AddCustomer;
using Domain.Features.Customer.GetCustomerInformation;
using Domain.Features.Transaction.ProcessTransactions;

namespace TransactionApi.Extensions;

public static class MediatorExtension
{
    public static void AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(GetCustomerInformationHandler).Assembly));
        
    }
}