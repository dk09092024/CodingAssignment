using Domain.Model;

namespace Domain.Repositories;

public interface ICustomerRepository : IRepository<Customer,Guid>
{
    public Task AddAsync(Customer customer);
    public Task<Customer> GetCustomerInformationAsync(Guid requestCustomerId, bool? IsIncludingAccounts = false);
}