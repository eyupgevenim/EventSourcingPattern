namespace Sample.Producer.API.Model
{
    using Sample.Producer.API.IntegrationEvents.Events;

    public class OrderStatusChangedModel
    {
        public int OrderId { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
