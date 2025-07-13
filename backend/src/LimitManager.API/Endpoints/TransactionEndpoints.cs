using LimitManager.Application.Dtos.Requests;
using LimitManager.Application.Interfaces.Services;
using LimitManager.Shared.ResultPattern;
using Microsoft.AspNetCore.Mvc;

namespace LimitManager.API.Endpoints;

public static class TransactionEndpoints
{
    public static IEndpointRouteBuilder UseTransactionEndpoints(this IEndpointRouteBuilder builder)
    {
        var transactionEndpoints = builder.MapGroup("/transaction");

        transactionEndpoints.MapPost("/pix",
            async ([FromBody] CreatePixTransactionRequest request,
                [FromServices] ITransactionAppService transactionAppService) =>
            {
                var result = await transactionAppService.CreatePixTransaction(request);
                return result.Match(() => Results.Ok(), CustomResults.Problem);
            });
        
        return transactionEndpoints;
    }
}