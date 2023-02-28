using MediatR;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages;
using NerdStore.Core.Messages.CommonMessages.DomainEvents;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.Sales.Application.Events;
using NerdStore.Sales.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Sales.Application.Commands
{
    public class OrderCommandHandler : IRequestHandler<AddItemToOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public OrderCommandHandler(IOrderRepository orderRepository, IMediatorHandler mediatorHandler)
        {
            _orderRepository = orderRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<bool> Handle(AddItemToOrderCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;

            var order = await _orderRepository.GetOrderDraftByClientId(message.ClientId);
            var orderItem = new OrderItem(message.ProductId, message.Name, message.Quantity, message.UnitValue);

            if(order == null)
            {
                order = Order.OrderFactory.NewOrderDraft(message.ClientId);
                order.AddItem(orderItem);

                _orderRepository.Add(order);
                order.AddEvent(new InitiatedOrderDraftEvent(message.ClientId, message.ProductId));
            }
            else
            {
                var orderItemExists = order.OrderItemExists(orderItem);
                order.AddItem(orderItem);

                if(orderItemExists) 
                {
                    _orderRepository.UpdateItem(order.OrderItems.FirstOrDefault(p => p.ProductId == orderItem.ProductId));
                }
                else
                {
                    _orderRepository.AddItem(orderItem);
                }

                order.AddEvent(new UpdatedOrderEvent(order.ClientId, order.Id, order.TotalValue));
            }

            order.AddEvent(new AddedItemToOrderEvent(order.ClientId, order.Id, message.ProductId, message.Name, message.UnitValue, message.Quantity));
            return await _orderRepository.UnitOfWork.Commit();
        }

        private bool ValidateCommand(Command message)
        {
            if (message.ItsValid()) return true;

            foreach(var error in message.ValidationResult.Errors)
            {
                _mediatorHandler.PublishNotification(new DomainNotification(message.MessageType, error.ErrorMessage));
            }
            return false;
        }
    }
}
