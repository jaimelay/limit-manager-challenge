using Amazon.DynamoDBv2.DataModel;

namespace LimitManager.Domain.Transaction;

[DynamoDBTable("transactions")]
public class Transaction
{
    [DynamoDBHashKey("pk")]
    public string PartitionKey { get; private set; }
    
    [DynamoDBRangeKey("sk")]
    public string SortKey { get; private set; }

    [DynamoDBProperty("transaction_id")] 
    public string Id { get; private set; } = Guid.NewGuid().ToString();
    
    [DynamoDBProperty("type")]
    public TransactionTypeEnum Type { get; private set; }
    
    [DynamoDBProperty("from_account_id")]
    public string FromAccountId { get; private set; }
    
    [DynamoDBProperty("to_account_id")]
    public string ToAccountId { get; private set; }
    
    [DynamoDBProperty("amount")]
    public decimal Amount { get; private set; }
    
    [DynamoDBProperty("created_at")]
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public static Transaction? CreatePixTransaction(Account.Account fromAccount, Account.Account toAccount, decimal amount)
    {
        if (!fromAccount.CanSendPix(amount))
        {
            return null;
        }

        fromAccount.SendPix(amount);
        toAccount.ReceivePix(amount);
        
        var transactionId = Guid.NewGuid().ToString();
        var createdAt = DateTime.UtcNow;
        
        return new Transaction
        {
            PartitionKey = BuildFromPk(fromAccount.Id),
            SortKey = BuildSortKey(createdAt, transactionId),
            Id = transactionId,
            Type = TransactionTypeEnum.Pix,
            FromAccountId = fromAccount.Id,
            ToAccountId = toAccount.Id,
            Amount = amount,
            CreatedAt = createdAt
        };
    }
    
    public static string BuildFromPk(string fromAccountId) => $"FROM#{fromAccountId}";
    public static string BuildToPk(string toAccountId) => $"TO#{toAccountId}";
    public static string BuildSortKey(DateTime createdAt, string transactionId) => $"{createdAt:O}#{transactionId}";
}