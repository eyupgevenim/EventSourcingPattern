namespace Sample.Consumer.API.Controllers
{
    using EventBus;
    using Microsoft.AspNetCore.Mvc;
    using Sample.Consumer.API.IntegrationEvents.Events;
    using System.Text.Json;

    [Route("v1/[controller]")]
    public class ProductController : Controller
    {
        private readonly ILogger<OrderController> _logger;

        public ProductController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }

        [Subscribe("CreatedProductConsumerIntegrationEvent")]
        [HttpPost("CreatedProductConsumer")]
        public async Task<IActionResult> CreatedProductConsumer(CreatedProductConsumerIntegrationEvent @event)
        {
            //TODO:....

            _logger.LogInformation($"ChangedOrderStatusConsumer - @event: {JsonSerializer.Serialize(@event)}");

            return Ok();
        }


    }
}