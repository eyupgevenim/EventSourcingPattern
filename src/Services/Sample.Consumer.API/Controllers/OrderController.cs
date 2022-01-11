namespace Sample.Consumer.API.Controllers
{
    using EventBus;
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

        [Subscribe("OrderStatusChangedIntegrationEvent")]
        [HttpPost("ChangedOrderStatusConsumer2")]
        public async Task<IActionResult> ChangedOrderStatusConsumer2(OrderStatusChangedIntegrationEvent @event)
        {
            //TODO:....

            _logger.LogInformation($"ChangedOrderStatusConsumer2 - @event: {JsonSerializer.Serialize(@event)}");

            return Ok();
        }

        [Subscribe("CreateOrderIntegrationEvent", isDynamic:true, nameSpace: "Sample.Consumer.API.IntegrationEvents.Events")]
        [HttpPost("CreateOrderConsumer")]
        public async Task<IActionResult> CreateOrderConsumer(string value)
        {
            //TODO:....

            _logger.LogInformation($"CreateOrderConsumer - value: {value}");

            return Ok();
        }

    }
}