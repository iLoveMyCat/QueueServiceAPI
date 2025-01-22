namespace QueueServiceAPI.Core
{
    public interface ICustomQueue
    {
        void Enqueue(string item);
        string Dequeue();
        string GetSnapshot();
    }
}
