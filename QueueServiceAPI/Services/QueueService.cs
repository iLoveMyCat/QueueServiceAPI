using QueueServiceAPI.Core;

namespace QueueServiceAPI.Services
{
    public class QueueService : IQueueService
    {
        private readonly Dictionary<string, ICustomQueue> _queues;

        public QueueService()
        {
            _queues = new Dictionary<string, ICustomQueue>();
            _queues.Add("defaultQueue", new CustomQueue());
        }

        public void Enqueue(string queueName, string item)
        {
            if (!_queues.ContainsKey(queueName))
            {
                _queues.Add(queueName, new CustomQueue());
            }

            _queues[queueName].Enqueue(item);
        }

        public string Dequeue(string queueName)
        {
            if (!_queues.ContainsKey(queueName))
            {
                throw new KeyNotFoundException("queue does not exist.");
            }
            if (_queues[queueName].IsEmpty())
            {
                throw new InvalidOperationException("queue is empty.");
            }
            return _queues[queueName].Dequeue();
        }

        public IEnumerable<string> GetSnapshot(string queueName)
        {
            if (!_queues.ContainsKey(queueName))
            {
                throw new KeyNotFoundException("queue does not exist.");
            }

            return _queues[queueName].GetSnapshot();
        }
    }
}
