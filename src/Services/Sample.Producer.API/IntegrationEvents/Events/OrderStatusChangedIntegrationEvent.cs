namespace Sample.Producer.API.IntegrationEvents.Events
{
    using EventBus.Events;

    public class OrderStatusChangedIntegrationEvent : IntegrationEvent
    {
        public OrderStatusChangedIntegrationEvent(int orderId, OrderStatus orderStatus)
        {
            OrderId = orderId;
            OrderStatus = orderStatus;
        }

        public int OrderId { get;}
        public OrderStatus OrderStatus { get;}
    }

    [Flags]
    public enum OrderStatus
    {
        None = 0,
        Approved = 1,
        Shipped = 2,
        Delivered = 4,
        Canceled = 8,
        //TODO:...
    }
}
