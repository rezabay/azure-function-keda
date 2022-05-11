using Demo.Common.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Demo.AzureFunction.Triggers;

public class QueueDemo
{
    [FunctionName("QueueTrigger")]
    public async Task Run(
        [QueueTrigger("demo-queue", Connection = "AzureWebJobsStorage")]
        QueueItem queueItem,
        ILogger logger)
    {
        await Task.Delay(2000);
        logger.LogInformation($"Completed processing: {queueItem.Id}");
    }
}