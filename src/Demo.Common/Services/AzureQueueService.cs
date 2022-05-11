using Azure.Storage.Queues;
using Demo.Common.Abstracts;
using Demo.Common.Helpers;
using Microsoft.Extensions.Options;

namespace Demo.Common.Services;

public class AzureQueueService : IQueueService
{
    private readonly string _connectionString;

    public AzureQueueService(IOptions<StorageConfig> configOptions)
    {
        _connectionString = configOptions.Value.ConnectionString;
    }

    public void CreateQueue(string queueName)
    {
        var queueClient = new QueueClient(_connectionString, queueName);
        queueClient.CreateIfNotExists();
    }

    public Task SendMessageAsync(string queueName, object messageObj)
    {
        var queueClient = new QueueClient(_connectionString, queueName);
        var json = JsonHelper.Serialize(messageObj);
        
        return queueClient.SendMessageAsync(json);
    }
}