using LimitManager.Application.Dtos.Requests;
using LimitManager.Shared.ResultPattern;

namespace LimitManager.Application.Interfaces.Services;

public interface ITransactionAppService
{
    Task<Result> CreatePixTransaction(CreatePixTransactionRequest request);
}