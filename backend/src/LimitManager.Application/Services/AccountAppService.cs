using LimitManager.Application.Dtos.Requests;
using LimitManager.Application.Dtos.Response;
using LimitManager.Application.Interfaces.Repositories;
using LimitManager.Application.Interfaces.Services;
using LimitManager.Domain.Account;
using LimitManager.Shared.ResultPattern;

namespace LimitManager.Application.Services;

public class AccountAppService(IAccountRepository accountRepository) : IAccountAppService
{
    public async Task<Result> CreateAccountAsync(CreateAccountRequest request)
    {
        var account = await accountRepository.GetByCompositeKeyAsync(request.Cpf, request.Agency, request.AccountNumber);

        if (account is not null)
        {
            return Result.Failure(Error.Conflict("Account.AlreadyExists", "Account already exists"));
        }
        
        var newAccount = Account.Create(request.Cpf, request.Agency, request.AccountNumber, request.Balance, request.PixLimit);

        await accountRepository.CreateAsync(newAccount);

        return Result.Success();
    }

    public async Task<Result<GetAccountPixLimitResponse>> GetAccountPixLimitAsync(GetAccountPixLimitRequest request)
    {
        var account = await accountRepository.GetByCompositeKeyAsync(request.Cpf, request.Agency, request.AccountNumber);

        return account is null 
            ? Result.Failure<GetAccountPixLimitResponse>(Error.NotFound("Accounts.NotFound", "Account not found.")) 
            : Result.Success(new GetAccountPixLimitResponse(request.Cpf, request.Agency, request.AccountNumber, account.PixLimit));
    }

    public async Task<Result> UpdateAccountPixLimitAsync(UpdateAccountPixLimitRequest request)
    {
        var account = await accountRepository.GetByCompositeKeyAsync(request.Cpf, request.Agency, request.AccountNumber);
        
        if (account is null)
        {
            return Result.Failure(Error.NotFound("Accounts.NotFound", "Account not found."));
        }
        
        account.SetPixLimit(request.PixLimit);
        
        await accountRepository.UpdateAsync(account);

        return Result.Success();
    }

    public async Task<Result> DeleteAccountAsync(DeleteAccountRequest request)
    {
        var account = await accountRepository.GetByCompositeKeyAsync(request.Cpf, request.Agency, request.AccountNumber);
        
        if (account is null)
        {
            return Result.Failure(Error.NotFound("Accounts.NotFound", "Account not found."));
        }

        await accountRepository.DeleteAsync(account);

        return Result.Success();
    }
}