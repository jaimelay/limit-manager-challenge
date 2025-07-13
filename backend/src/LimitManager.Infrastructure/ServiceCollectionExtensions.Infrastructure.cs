using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using LimitManager.Application.Interfaces.Repositories;
using LimitManager.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace LimitManager.Infrastructure;

public static class ServiceCollectionExtensionsInfrastructure
{
    public static void AddInfrastructureLayer(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        AddAmazonDynamoDb(services);
        AddRepositories(services);
    }

    private static void AddAmazonDynamoDb(IServiceCollection services)
    {
        services.AddSingleton<IAmazonDynamoDB>(_ => new AmazonDynamoDBClient(RegionEndpoint.USEast1));
        services.AddSingleton<IDynamoDBContext, DynamoDBContext>();
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
    }
}