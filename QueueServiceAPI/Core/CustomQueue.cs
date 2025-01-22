namespace QueueServiceAPI.Core
{
    public class CustomQueue : ICustomQueue
    {
        private LinkedList<string> _queue = new LinkedList<string>();

        public void Enqueue(string item)
        {
            if (string.IsNullOrEmpty(item))
            {
                throw new ArgumentException("item cannot be null");
            }
            _queue.AddLast(item);
        }

        public string Dequeue()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("queue is empty.");
            }

            var item = _queue.First.Value;
            _queue.RemoveFirst();
            return item;
        }

        public IEnumerable<string> GetSnapshot()
        {
            return new List<string>(_queue);
        }

        public bool IsEmpty()
        {
            return _queue.Count == 0;
        }
    }
}
