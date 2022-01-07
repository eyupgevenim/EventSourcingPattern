namespace Sample.Consumer.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Sample.Consumer.API.IntegrationEvents.Events;
    using System.Text.Json;

    [Route("v1/[controller]")]
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }

        [HttpPost("ChangedOrderStatusConsumer")]
        public async Task<IActionResult> ChangedOrderStatusConsumer(OrderStatusChangedIntegrationEvent @event)
        {
            //TODO:....

            _logger.LogInformation($"ChangedOrderStatusConsumer - @event: {JsonSerializer.Serialize(@event)}");

            return Ok();
        }

    }
}