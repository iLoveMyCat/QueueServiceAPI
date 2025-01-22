﻿namespace QueueServiceAPI.Services
{
    public interface IQueueService
    {
        void Enqueue(string queueName, string item);
        string Dequeue(string queueName);
        string GetSnapshot(string queueName);
    }
}
