using LimitManager.Application.Dtos.Requests;
using LimitManager.Application.Dtos.Response;
using LimitManager.Shared.ResultPattern;

namespace LimitManager.Application.Interfaces.Services;

public interface IAccountAppService
{
    Task<Result> CreateAccountAsync(CreateAccountRequest request);

    Task<Result<GetAccountPixLimitResponse>> GetAccountPixLimitAsync(GetAccountPixLimitRequest request);

    Task<Result> UpdateAccountPixLimitAsync(UpdateAccountPixLimitRequest request);

    Task<Result> DeleteAccountAsync(DeleteAccountRequest request);
}