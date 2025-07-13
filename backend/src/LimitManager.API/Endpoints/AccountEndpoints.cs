using LimitManager.Application.Dtos.Requests;
using LimitManager.Application.Interfaces.Services;
using LimitManager.Shared.ResultPattern;
using Microsoft.AspNetCore.Mvc;

namespace LimitManager.API.Endpoints;

public static class AccountEndpoints
{
    public static IEndpointRouteBuilder UseAccountEndpoints(this IEndpointRouteBuilder builder)
    {
        var accountEndpoints = builder.MapGroup("/account");

        accountEndpoints.MapPost("/",
            async ([FromBody] CreateAccountRequest request, [FromServices] IAccountAppService accountAppService) =>
            {
                var result = await accountAppService.CreateAccountAsync(request);
                return result.Match(() => Results.Ok(), CustomResults.Problem);
            });

        accountEndpoints.MapGet("/pixlimit",
            async ([FromQuery] string cpf, [FromQuery] string agency, [FromQuery] string accountNumber,
                [FromServices] IAccountAppService accountAppService) =>
            {
                var result = await accountAppService.GetAccountPixLimitAsync(new GetAccountPixLimitRequest(cpf, agency, accountNumber));
                return result.Match(Results.Ok, CustomResults.Problem);
            });
        
        accountEndpoints.MapPut("/pixlimit",
            async ([FromBody] UpdateAccountPixLimitRequest request,
                [FromServices] IAccountAppService accountAppService) =>
            {
                var result = await accountAppService.UpdateAccountPixLimitAsync(request);
                return result.Match(() => Results.Ok(), CustomResults.Problem);
            });

        accountEndpoints.MapDelete("/",
            async ([FromBody] DeleteAccountRequest request, [FromServices] IAccountAppService accountAppService) =>
            {
                var result = await accountAppService.DeleteAccountAsync(request);
                return result.Match(() => Results.Ok(), CustomResults.Problem);
            });
        
        return accountEndpoints;
    }
}