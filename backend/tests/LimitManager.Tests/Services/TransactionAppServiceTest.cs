using Amazon.DynamoDBv2.DataModel;
using LimitManager.Application.Dtos.Requests;
using LimitManager.Application.Interfaces.Repositories;
using LimitManager.Application.Interfaces.Services;
using LimitManager.Application.Services;
using LimitManager.Domain.Account;
using NSubstitute;
using Xunit;

namespace LimitManager.Tests.Services;

public class TransactionAppServiceTest
{
    private readonly ITransactionAppService _sut;
    private readonly IAccountRepository _accountRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IDynamoDBContext _dynamoDbContext;
    
    public TransactionAppServiceTest()
    {
        _accountRepository = Substitute.For<IAccountRepository>();
        _transactionRepository = Substitute.For<ITransactionRepository>();
        _dynamoDbContext = Substitute.For<IDynamoDBContext>();
        
        _sut = new TransactionAppService(_accountRepository, _transactionRepository, _dynamoDbContext);
    }
    
    [Fact]
    public async Task ShouldNotBeAbleToCreateATransactionIfAmountIsGreaterThanPixLimit()
    {
        // Arrange
        var fromAccountCpf = "111";
        var fromAccountAgency = "111";
        var fromAccountAccountNumber = "111";
        var fromAccount = Account.Create(fromAccountCpf, fromAccountAgency, fromAccountAccountNumber, 1000, 200);
        
        var toAccountCpf = "222";
        var toAccountAgency = "222";
        var toAccountAccountNumber = "222";
        var toAccount = Account.Create(toAccountCpf, toAccountAgency, toAccountAccountNumber, 1000, 5000);
        
        var mock = new CreatePixTransactionRequest
        {
            FromAccount = new CreatePixTransactionRequest.Account
            {
                Cpf = fromAccountCpf,
                Agency = fromAccountAgency,
                AccountNumber = fromAccountAccountNumber
            }, 
            ToAccount = new CreatePixTransactionRequest.Account
            {
                Cpf = toAccountCpf,
                Agency = toAccountAgency,
                AccountNumber = toAccountAccountNumber
            },
            Amount = 800
        };

        _accountRepository.GetByCompositeKeyAsync(fromAccountCpf, fromAccountAgency, fromAccountAccountNumber).Returns(fromAccount);
        _accountRepository.GetByCompositeKeyAsync(toAccountCpf, toAccountAgency, toAccountAccountNumber).Returns(toAccount);
        
        // Act
        var result = await _sut.CreatePixTransaction(mock);

        // Assert
        Assert.True(result.IsFailure);
    }
}