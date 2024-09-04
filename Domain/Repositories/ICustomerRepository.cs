using Domain.Model;

namespace Domain.Repositories;

public interface ICustomerRepository : IRepository<Customer,Guid>
{
    Task AddAsync(Customer customer);
}