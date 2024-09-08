using Domain.Model;

namespace Domain.Repositories;

public interface ICustomerRepository : IRepository<Customer,Guid>
{
    public Task AddAsync(Customer customer,CancellationToken? cancellationToken = null);
    public Task<Customer> GetCustomerInformationAsync(Guid requestCustomerId, bool? isIncludingAccounts = false,CancellationToken? cancellationToken = null);
}