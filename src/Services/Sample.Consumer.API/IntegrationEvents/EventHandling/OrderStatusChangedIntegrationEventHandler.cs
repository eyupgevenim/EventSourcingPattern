namespace Sample.Consumer.API.IntegrationEvents.EventHandling
{
    using EventBus.Abstractions;
    using Sample.Consumer.API.Controllers;
    using Sample.Consumer.API.IntegrationEvents.Events;
    using System.Threading.Tasks;

    public class OrderStatusChangedIntegrationEventHandler : IIntegrationEventHandler<OrderStatusChangedIntegrationEvent>
    {
        private readonly OrderController orderController;

        public OrderStatusChangedIntegrationEventHandler(OrderController orderController)
        {
            this.orderController = orderController;
        }

        public Task Handle(OrderStatusChangedIntegrationEvent @event)
        {
            return Task.FromResult(orderController.ChangedOrderStatusConsumer(@event));
        }
    }
}
