using NerdStore.Core.Messages;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Sales.Application.Events
{
    public class InitiatedOrderDraftEvent : Event
    {
        public Guid ClientId { get; private set; }
        public Guid OrderId { get; private set; }

        public InitiatedOrderDraftEvent(Guid clientId, Guid orderId)
        {
            AggregateId = orderId;
            ClientId = clientId;
            OrderId = orderId;
        }

    }
}
