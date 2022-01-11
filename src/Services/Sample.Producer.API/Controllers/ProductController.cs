namespace Sample.Producer.API.Controllers
{
    using EventBus.Abstractions;
    using Microsoft.AspNetCore.Mvc;
    using Sample.Producer.API.IntegrationEvents.Events;

    [Route("v1/[controller]")]
    public class ProductController : Controller
    {
        private readonly IEventBus eventBus;
        private readonly ILogger<OrderController> _logger;

        public ProductController(ILogger<OrderController> logger, 
            IEventBus eventBus)
        {
            _logger = logger;
            this.eventBus = eventBus;
        }

        [HttpPost("CreateProductPublish")]
        public async Task<IActionResult> CreateProductPublish(CreatedProductConsumerIntegrationEvent @event)
        {
            //TODO:....

            eventBus.Publish(@event);

            _logger.LogInformation("CreateProductPublish - @event:{@event}", @event);
            //TODO:..

            return Ok();
        }


    }
}