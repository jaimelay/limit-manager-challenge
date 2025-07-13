using Amazon.DynamoDBv2.DataModel;
using LimitManager.Application.Interfaces.Repositories;
using LimitManager.Domain.Account;

namespace LimitManager.Infrastructure.Repositories;

public class AccountRepository(IDynamoDBContext dynamoDbContext) : IAccountRepository
{
    public async Task CreateAsync(Account account)
    {
        await dynamoDbContext.SaveAsync(account);
    }

    public async Task UpdateAsync(Account account)
    {
        await dynamoDbContext.SaveAsync(account);
    }

    public async Task DeleteAsync(Account account)
    {
        var pk = Account.BuildPartitionKey(account.Cpf, account.Agency, account.AccountNumber);
        var sk = Account.BuildSortKey(account.Cpf, account.Agency, account.AccountNumber);
        
        await dynamoDbContext.DeleteAsync<Account>(pk, sk);
    }
    
    public async Task<Account?> GetByCompositeKeyAsync(string cpf, string agency, string accountNumber)
    {
        var pk = Account.BuildPartitionKey(cpf, agency, accountNumber);
        var sk = Account.BuildSortKey(cpf, agency, accountNumber);
        
        return await dynamoDbContext.LoadAsync<Account>(pk, sk);
    }
}