using Demo.Common;
using Demo.Common.Abstracts;
using Demo.Common.Services;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder;

public static class ServiceCollectionExtensions
{
    public static void AddStorageServices(this IServiceCollection services, Action<StorageConfig> configAction)
    {
        services.Configure(configAction);
        
        services.AddSingleton<IBlobStorageService, AzureBlobStorageService>();
        services.AddSingleton<IQueueService, AzureQueueService>();
    }
}