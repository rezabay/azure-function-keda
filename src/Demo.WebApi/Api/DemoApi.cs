using Demo.Common;
using Demo.Common.Abstracts;
using Demo.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Demo.WebApi.Api;

[ApiController]
[Route("api/demo")]
public class DemoApi : Controller
{
    private readonly IQueueService _queueService;
    private readonly StorageConfig _storageConfig;

    public DemoApi(IQueueService queueService, IOptions<StorageConfig> storageOptions)
    {
        ArgumentNullException.ThrowIfNull(storageOptions.Value);
        
        _queueService = queueService;
        _storageConfig = storageOptions.Value;
    }

    [HttpPost]
    public async Task<IActionResult> Post()
    {
        _queueService.CreateQueue(_storageConfig.DemoQueueName);

        for (var i = 1; i <= 500; i++)
        {
            await _queueService.SendMessageAsync(_storageConfig.DemoQueueName,
                new QueueItem
                {
                    Id = i,
                    Data = $"Data: {i}"
                });
        }
        
        return Ok();
    }
}