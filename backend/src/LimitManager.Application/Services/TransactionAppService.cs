using Amazon.DynamoDBv2.DataModel;
using LimitManager.Application.Dtos.Requests;
using LimitManager.Application.Interfaces.Repositories;
using LimitManager.Application.Interfaces.Services;
using LimitManager.Domain.Account;
using LimitManager.Domain.Transaction;
using LimitManager.Shared.ResultPattern;

namespace LimitManager.Application.Services;

public class TransactionAppService(IAccountRepository accountRepository, ITransactionRepository transactionRepository, IDynamoDBContext dynamoDbContext) : ITransactionAppService
{
    public async Task<Result> CreatePixTransaction(CreatePixTransactionRequest request)
    {
        try
        {
            var fromAccount = await accountRepository.GetByCompositeKeyAsync(request.FromAccount.Cpf,
                request.FromAccount.Agency, request.FromAccount.AccountNumber);
            var toAccount = await accountRepository.GetByCompositeKeyAsync(request.ToAccount.Cpf,
                request.ToAccount.Agency, request.ToAccount.AccountNumber);

            if (fromAccount is null)
                return Result.Failure(Error.NotFound("Transaction.FromAccountNotFound",
                    "The account used to send pix was not found."));

            if (toAccount is null)
                return Result.Failure(Error.NotFound("Transaction.ToAccountNotFound",
                    "The receiver account was not found."));

            if (fromAccount == toAccount)
            {
                return Result.Failure(Error.NotFound("Transaction.SameAccount",
                    "It's not possible to make a transaction to the same account."));
            }
            
            if (!fromAccount.CanSendPix(request.Amount))
                return Result.Failure(Error.NotFound("Account.CannotSendPix",
                    "The account has no Pix limit to make a transaction."));

            var transaction = Transaction.CreatePixTransaction(fromAccount, toAccount, request.Amount);

            if (transaction is null)
                return Result.Failure(Error.NotFound("Transaction.Failure", "The transaction could not be created."));

            var accounts = dynamoDbContext.CreateTransactWrite<Account>();
            accounts.AddSaveItems([fromAccount, toAccount]);
            
            var transactions = dynamoDbContext.CreateTransactWrite<Transaction>();
            transactions.AddSaveItem(transaction);

            var dynamoTransaction = dynamoDbContext.CreateMultiTableTransactWrite(accounts, transactions);

            await dynamoTransaction.ExecuteAsync();

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(Error.NotFound("Transaction.Failure", ex.Message));
        }
    }
}