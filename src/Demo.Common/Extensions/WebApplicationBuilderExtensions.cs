using Serilog;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder;

public static class WebApplicationBuilderExtensions
{
    public static void SetupLogger(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, _, configuration) => configuration
                                                              .ReadFrom.Configuration(context.Configuration)
                                                              .Enrich.FromLogContext());
    }
}