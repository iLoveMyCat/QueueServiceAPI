using Microsoft.AspNetCore.Mvc;
using QueueServiceAPI.Models;
using QueueServiceAPI.Services;

namespace QueueServiceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QueueController : Controller
    {
        private readonly IQueueService _queueService;

        public QueueController(IQueueService queueService)
        {
            _queueService = queueService;
        }

        [HttpPost]
        public IActionResult Enqueue([FromBody] EnqueueRequest request)
        {
            try
            {
                _queueService.Enqueue(request.QueueName, request.Item);
                return Ok(); 
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message); 
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred."); 
            }
        }

        [HttpDelete]
        public IActionResult Dequeue([FromBody] DequeueRequest request)
        {
            try
            {
                var item = _queueService.Dequeue(request.QueueName);
                if (item == null)
                {
                    return NotFound("No items in the queue.");
                }
                return Ok(item);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message); 
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred."); 
            }
        }

        [HttpGet]
        public IActionResult GetSnapshot([FromBody] SnapshotRequest request)
        {
            try
            {
                var snapshot = _queueService.GetSnapshot(request.QueueName);
                if (snapshot == null)
                {
                    return NotFound("Queue not found."); 
                }
                return Ok(snapshot); 
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message); 
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
