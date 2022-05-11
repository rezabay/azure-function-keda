namespace Demo.Common.Abstracts;

public interface IQueueService
{
    void CreateQueue(string queueName);
    Task SendMessageAsync(string queueName, object messageObj);
}