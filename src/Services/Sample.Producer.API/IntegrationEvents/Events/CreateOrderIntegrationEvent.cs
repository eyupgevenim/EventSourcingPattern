namespace Sample.Producer.API.IntegrationEvents.Events
{
    using EventBus.Events;

    public class CreateOrderIntegrationEvent : IntegrationEvent
    {
        public CreateOrderIntegrationEvent(int orderId, decimal price)
        {
            OrderId = orderId;
            Price = price;
        }

        public int OrderId { get; }
        public decimal Price { get; }
    }
}
