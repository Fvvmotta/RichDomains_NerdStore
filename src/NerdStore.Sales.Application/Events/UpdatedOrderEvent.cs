using NerdStore.Core.Messages;

namespace NerdStore.Sales.Application.Events
{
    public class UpdatedOrderEvent : Event
    {
        public Guid ClientId { get; private set; }
        public Guid OrderId { get; private set; }
        public decimal TotalValue { get; private set; }

        public UpdatedOrderEvent(Guid clientId, Guid orderId, decimal totalValue)
        {
            AggregateId = orderId;
            ClientId = clientId;
            OrderId = orderId;
            TotalValue = totalValue;
        }

    }
}
