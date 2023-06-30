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
    public class OrderCommandHandler : 
        IRequestHandler<AddItemOrderCommand, bool>,
        IRequestHandler<UpdateItemOrderCommand, bool>,
        IRequestHandler<RemoveItemOrderCommand, bool>,
        IRequestHandler<ApplyVoucherOrderCommand, bool>
    {
       
        private readonly IOrderRepository _orderRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public OrderCommandHandler(IOrderRepository orderRepository, IMediatorHandler mediatorHandler)
        {
            _orderRepository = orderRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<bool> Handle(AddItemOrderCommand message, CancellationToken cancellationToken)
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

            order.AddEvent(new AddedItemOrderEvent(order.ClientId, order.Id, message.ProductId, message.Name, message.UnitValue, message.Quantity));
            return await _orderRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(UpdateItemOrderCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;

            var order = await _orderRepository.GetOrderDraftByClientId(message.ClientId);

            if (order == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("order", "Order Not Found!"));
                return false;
            }

            var orderItem = await _orderRepository.GetItemByOrder(order.Id, message.ProductId);

            if (!order.OrderItemExists(orderItem))
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("order", "Order item not found!"));
                return false;
            }

            order.UpdateUnits(orderItem, message.Quantity);
            order.AddEvent(new UpdatedOrderEvent(message.ClientId, order.Id, order.TotalValue));

            _orderRepository.UpdateItem(orderItem);
            _orderRepository.Update(order);

            return await _orderRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(RemoveItemOrderCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;

            var pedido = await _orderRepository.GetOrderDraftByClientId(message.ClienteId);

            if (pedido == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("pedido", "Pedido não encontrado!"));
                return false;
            }

            var pedidoItem = await _orderRepository.GetItemByOrder(pedido.Id, message.ProdutoId);

            if (pedidoItem != null && !pedido.OrderItemExists(pedidoItem))
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("pedido", "Item do pedido não encontrado!"));
                return false;
            }

            pedido.RemoveItem(pedidoItem);
            pedido.AddEvent(new RemoveItemOrderEvent(message.ClienteId, pedido.Id, message.ProdutoId));

            _orderRepository.RemoveItem(pedidoItem);
            _orderRepository.Update(pedido);

            return await _orderRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(ApplyVoucherOrderCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;

            var order = await _orderRepository.GetOrderDraftByClientId(message.ClienteId);

            if (order == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("order", "Order not found!"));
                return false;
            }

            var voucher = await _orderRepository.GetVoucherByCode(message.CodigoVoucher);

            if (voucher == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("order", "Voucher not found!"));
                return false;
            }

            var applyVoucherValidation = order.ApplyVoucher(voucher);

            if (!applyVoucherValidation.IsValid)
            {
                foreach (var error in applyVoucherValidation.Errors)
                {
                    await _mediatorHandler.PublishNotification(new DomainNotification(error.ErrorCode, error.ErrorMessage));
                }

                return false;
            }

            order.AddEvent(new ApplyVoucherOrderEvent(message.ClienteId, order.Id, voucher.Id));

            _orderRepository.Update(order);

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
