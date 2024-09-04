using Domain.Model;

namespace Domain.Repositories;

public interface ITransactionRepository : IRepository<Transaction,Guid>
{
    
}