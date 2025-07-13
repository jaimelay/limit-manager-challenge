using LimitManager.Domain.Transaction;

namespace LimitManager.Application.Interfaces.Repositories;

public interface ITransactionRepository
{
    Task CreateAsync(Transaction transaction);
}