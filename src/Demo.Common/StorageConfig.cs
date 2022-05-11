namespace Demo.Common;

public class StorageConfig
{
    public string ConnectionString { get; set; } = null!;
    public string DemoQueueName { get; set; } = "demo-queue";
}