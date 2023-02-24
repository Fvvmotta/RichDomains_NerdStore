using NerdStore.Core.Messages.CommonMessages.DomainEvents;

namespace NerdStore.Catalog.Domain.Events
{
    public class ProductBelowInventoryEvent : DomainEvent
    {
        public int InventoryLeft { get; private set; }

        public ProductBelowInventoryEvent(Guid aggregateId, int inventoryLeft) : base(aggregateId)
        {
            InventoryLeft = inventoryLeft;
        }
    }
}
