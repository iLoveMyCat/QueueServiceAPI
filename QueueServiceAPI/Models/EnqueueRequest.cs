namespace QueueServiceAPI.Models
{
    public class EnqueueRequest
    {
        public string QueueName { get; set; } = "defaultQueue";
        public string Item { get; set; } 
    }
}
