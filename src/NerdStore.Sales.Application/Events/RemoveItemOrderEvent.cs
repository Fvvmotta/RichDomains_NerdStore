using NerdStore.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Sales.Application.Events
{
    public class RemoveItemOrderEvent : Event
    {
        public Guid ClientId { get; private set; }
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }

        public RemoveItemOrderEvent(Guid clientId, Guid orderId, Guid productId)
        {
            AggregateId = orderId;
            ClientId = clientId;
            OrderId = orderId;
            ProductId = productId;
        }
    }
}
