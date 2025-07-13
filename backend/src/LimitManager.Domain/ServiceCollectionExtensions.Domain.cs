using Microsoft.Extensions.DependencyInjection;

namespace LimitManager.Domain;

public static class ServiceCollectionExtensionsDomain
{
    public static void AddDomainLayer(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
    }
}