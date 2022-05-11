using System.Reflection;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Routing;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder;

public static class EndpointRouteBuilderExtensions
{
    public static void MapHealthChecks(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapHealthChecks("/health", new HealthCheckOptions
                                             {
                                                 Predicate = _ => false
                                             });
        endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions
                                                   {
                                                       Predicate = check => check.Tags.Contains("ready")
                                                   });
    }
    
    public static void MapVersionApi(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/version", () =>
                                     {
                                         var assemblyName = Assembly.GetExecutingAssembly().GetName();
                                         var version = assemblyName.Version?.ToString();

                                         return new { Version = version };
                                     });
    }
}