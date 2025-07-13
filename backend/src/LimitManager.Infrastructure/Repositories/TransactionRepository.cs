using Amazon.DynamoDBv2.DataModel;
using LimitManager.Application.Interfaces.Repositories;
using LimitManager.Domain.Transaction;

namespace LimitManager.Infrastructure.Repositories;

public class TransactionRepository(IDynamoDBContext dynamoDbContext) : ITransactionRepository
{
    public async Task CreateAsync(Transaction transaction)
    {
        await dynamoDbContext.SaveAsync(transaction);
    }
}