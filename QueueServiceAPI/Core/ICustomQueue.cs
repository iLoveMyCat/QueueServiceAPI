namespace QueueServiceAPI.Core
{
    public interface ICustomQueue
    {
        void Enqueue(string item);
        string Dequeue();
        IEnumerable<string> GetSnapshot();

        bool IsEmpty();
    }
}
