using System;
using System.Collections.Generic;
using QueueServiceAPI.Core;
using QueueServiceAPI.Services;
using Xunit;

namespace QueueServiceAPI.Test.Services
{
    public class QueueServiceTests
    {
        private QueueService _service;

        public QueueServiceTests()
        {
            _service = new QueueService();
        }

        [Fact]
        public void Enqueue_AddsItemToQueue()
        {
            _service.Enqueue("testQueue", "testItem");
            var snapshot = _service.GetSnapshot("testQueue");
            Assert.Contains("testItem", snapshot);
        }

        [Fact]
        public void Enqueue_CreatesNewQueueIfNotExists()
        {
            _service.Enqueue("newQueue", "newItem");
            var snapshot = _service.GetSnapshot("newQueue");
            Assert.Contains("newItem", snapshot);
        }

        [Fact]
        public void Dequeue_RemovesAndReturnsItem()
        {
            _service.Enqueue("testQueue", "testItem");
            var result = _service.Dequeue("testQueue");
            Assert.Equal("testItem", result);
        }

        [Fact]
        public void Dequeue_ThrowsIfQueueIsEmpty()
        {
            Assert.Throws<KeyNotFoundException>(() => _service.Dequeue("emptyQueue"));
        }

        [Fact]
        public void GetSnapshot_ReturnsItemsInQueue()
        {
            _service.Enqueue("testQueue", "item1");
            _service.Enqueue("testQueue", "item2");
            var snapshot = _service.GetSnapshot("testQueue");
            Assert.Equal(new List<string> { "item1", "item2" }, snapshot);
        }

        [Fact]
        public void GetSnapshot_ThrowsIfQueueDoesNotExist()
        {
            Assert.Throws<KeyNotFoundException>(() => _service.GetSnapshot("nonExistentQueue"));
        }
    }
}
