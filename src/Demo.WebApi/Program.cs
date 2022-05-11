var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

builder.SetupLogger();

services.AddHealthChecks();
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddStorageServices(config =>
{
    config.ConnectionString = configuration["AzureWebJobsStorage"];
});

var app = builder.Build();

app.MapHealthChecks();
app.UseAuthorization();
app.MapControllers();

app.Run();