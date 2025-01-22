using Microsoft.AspNetCore.Mvc;
using Moq;
using QueueServiceAPI.Controllers;
using QueueServiceAPI.Models;
using QueueServiceAPI.Services;
using Xunit;

namespace QueueServiceAPI.Test.Controllers
{
    public class QueueControllerTests
    {
        private Mock<IQueueService> _mockQueueService;
        private QueueController _controller;

        public QueueControllerTests()
        {
            _mockQueueService = new Mock<IQueueService>();
            _controller = new QueueController(_mockQueueService.Object);
        }

        [Fact]
        public void Enqueue_ReturnsOk()
        {
            var request = new EnqueueRequest { QueueName = "testQueue", Item = "testItem" };
            var result = _controller.Enqueue(request);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void Enqueue_ReturnsBadRequest()
        {
            var request = new EnqueueRequest { QueueName = "testQueue", Item = null };
            _mockQueueService.Setup(x => x.Enqueue(It.IsAny<string>(), It.IsAny<string>())).Throws(new ArgumentException());
            var result = _controller.Enqueue(request);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Dequeue_ReturnsItem()
        {
            var request = new DequeueRequest { QueueName = "testQueue" };
            _mockQueueService.Setup(x => x.Dequeue(request.QueueName)).Returns("testItem");
            var result = _controller.Dequeue(request);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Dequeue_ReturnsNotFound()
        {
            var request = new DequeueRequest { QueueName = "testQueue" };
            _mockQueueService.Setup(x => x.Dequeue(request.QueueName)).Throws(new InvalidOperationException());
            var result = _controller.Dequeue(request);
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
