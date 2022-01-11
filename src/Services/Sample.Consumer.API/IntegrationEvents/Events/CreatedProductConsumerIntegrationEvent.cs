using EventBus.Events;

namespace Sample.Consumer.API.IntegrationEvents.Events
{
    public class CreatedProductConsumerIntegrationEvent : IntegrationEvent
    {

        public string Title { get; set; }
    }
}
