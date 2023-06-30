using NerdStore.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Sales.Application.Events
{
    public class ApplyVoucherOrderEvent : Event
    {
        public Guid ClientId { get; private set; }
        public Guid OrderId { get; private set; }
        public Guid VoucherId { get; private set; }

        public ApplyVoucherOrderEvent(Guid clientId, Guid orderId, Guid voucherId)
        {
            AggregateId = orderId;
            ClientId = clientId;
            OrderId = orderId;
            VoucherId = voucherId;
        }
    }
}
