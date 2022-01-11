namespace Sample.Producer.API.IntegrationEvents.Events
{
    using EventBus.Events;

    public class CreatedProductConsumerIntegrationEvent : IntegrationEvent
    {
        public string Title { get; set; }
    }
}
