using Amazon.DynamoDBv2.DataModel;

namespace LimitManager.Domain.Account;

[DynamoDBTable("accounts")]
public class Account
{
    [DynamoDBHashKey("pk")] 
    public string PartitionKey { get; private set; }
    
    [DynamoDBRangeKey("sk")]
    public string SortKey { get; private set; }
    
    [DynamoDBProperty("account_id")]
    public string Id { get; private set; }
    
    [DynamoDBProperty("cpf")]
    public string Cpf { get; private set; }
    
    [DynamoDBProperty("agency")]
    public string Agency { get; private set; }
    
    [DynamoDBProperty("account_number")]
    public string AccountNumber { get; private set; }
    
    [DynamoDBProperty("balance")]
    public decimal Balance { get; private set; }
    
    [DynamoDBProperty("pix_limit")]
    public decimal PixLimit { get; private set; }
    
    public static Account Create(string cpf, string agency, string accountNumber, decimal balance, decimal pixLimit)
    {
        return new Account
        {
            PartitionKey = BuildPartitionKey(cpf, agency, accountNumber),
            SortKey = BuildSortKey(cpf, agency, accountNumber),
            Id = Guid.NewGuid().ToString(),
            Cpf = cpf,
            Agency = agency,
            AccountNumber = accountNumber,
            Balance = balance,
            PixLimit = pixLimit
        };
    }

    public static string BuildPartitionKey(string cpf, string agency, string accountNumber) => $"{cpf}#{agency}#{accountNumber}";
    
    public static string BuildSortKey(string cpf, string agency, string accountNumber) => $"{cpf}#{agency}#{accountNumber}";
    
    public bool CanSendPix(decimal amount)
    {
        if (PixLimit < amount)
        {
            throw new Exception("The pix limit is less than the transaction amount.");
        }

        if (Balance < amount)
        {
            throw new Exception("The balance is insufficient for this transaction amount.");
        }

        return true;
    }

    public void SendPix(decimal amount)
    {
        if (!CanSendPix(amount))
        {
            return;
        }
        
        PixLimit -= amount;
        Balance -= amount;
    }
    
    public void ReceivePix(decimal amount)
    {
        Balance += amount;
    }
    
    public void SetPixLimit(decimal pixLimit)
    {
        PixLimit = pixLimit;
    }
}