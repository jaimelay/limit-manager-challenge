using LimitManager.Domain.Account;

namespace LimitManager.Application.Interfaces.Repositories;

public interface IAccountRepository
{
    Task CreateAsync(Account account);
    Task UpdateAsync(Account account);
    Task DeleteAsync(Account account);
    Task<Account?> GetByCompositeKeyAsync(string cpf, string agency, string accountNumber);
}