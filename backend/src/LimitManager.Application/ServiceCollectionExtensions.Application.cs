using FluentValidation;
using LimitManager.Application.Dtos.Requests;
using LimitManager.Application.Interfaces.Services;
using LimitManager.Application.Services;
using LimitManager.Application.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace LimitManager.Application;

public static class ServiceCollectionExtensionsApplication
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        
        AddApplicationServices(services);
        AddValidators(services);
    }

    private static void AddApplicationServices(IServiceCollection services)
    {
        services.AddScoped<IAccountAppService, AccountAppService>();
        services.AddScoped<ITransactionAppService, TransactionAppService>();
    }
    
    private static void AddValidators(IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateAccountRequest>, CreateAccountRequestValidator>();
    }
}