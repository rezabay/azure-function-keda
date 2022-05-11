using System.Net;
using Demo.AzureFunction;
using Microsoft.AspNetCore.Builder;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]

namespace Demo.AzureFunction
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var services = builder.Services;
            var configuration = builder.GetContext().Configuration;
            
            services.AddOptions();
            services.AddLogging();
            
            services.AddStorageServices(config =>
                                        {
                                            config.ConnectionString = configuration["AzureWebJobsStorage"];
                                        });

            // Disable Nagle on the tcp stack
            ServicePointManager.UseNagleAlgorithm = false;
            ServicePointManager.Expect100Continue = false;
        }
    }
}