using LimitManager.API.Endpoints;

namespace LimitManager.API;

public static class ServiceCollectionExtensionsEndpoints
{
    
    public static IEndpointRouteBuilder UseEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var endpointGroup = endpoints.MapGroup("/api/v1");

        endpointGroup.UseAccountEndpoints();
        endpointGroup.UseTransactionEndpoints();

        return endpoints;
    }
}