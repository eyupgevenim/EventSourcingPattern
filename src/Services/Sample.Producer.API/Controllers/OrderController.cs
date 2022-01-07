namespace Sample.Producer.API.Controllers
{
    using EventBus.Abstractions;
    using Microsoft.AspNetCore.Mvc;
    using Sample.Producer.API.IntegrationEvents.Events;
    using Sample.Producer.API.Model;

    [Route("v1/[controller]")]
    public class OrderController : Controller
    {
        private readonly IEventBus eventBus;
        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger, 
            IEventBus eventBus)
        {
            _logger = logger;
            this.eventBus = eventBus;
        }

        [HttpPost("ChangedOrderStatusPublish")]
        public async Task<IActionResult> ChangedOrderStatusPublish(OrderStatusChangedModel model)
        {
            //TODO:....

            var @event = new OrderStatusChangedIntegrationEvent(model.OrderId, model.OrderStatus);
            eventBus.Publish(@event);

            _logger.LogInformation("ChangedOrderStatusPublish - @event:{@event}", @event);
            //TODO:..

            return Ok();
        }

    }
}