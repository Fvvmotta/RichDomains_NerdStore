using NerdStore.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Sales.Domain
{
    public class Order : Entity, IAggregateRoot
    {
        public int Code { get; private set; }
        public Guid ClientId { get; private set; }
        public Guid? VoucherId { get; private set; }
        public bool UsedVoucher { get; private set; }
        public decimal Discount { get; private set; }
        public decimal TotalValue { get; private set; }
        public DateTime RegisterDate { get; private set; }
        public OrderStatus OrderStatus { get; private set; }

        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        //EF Rel.
        public Voucher Voucher { get; private set; }

        public Order(Guid clientId, bool usedVoucher, decimal discount, decimal totalValue)
        {
            ClientId = clientId;
            UsedVoucher = usedVoucher;
            Discount = discount;
            TotalValue = totalValue;
            _orderItems = new List<OrderItem>();
        }
        protected Order()
        {
            _orderItems= new List<OrderItem>();
        }

        public void ApplyVoucher(Voucher voucher)
        {
            Voucher = voucher;
            UsedVoucher = true;
            CaculateOrderValue();
        }

        public void CaculateOrderValue()
        {
            TotalValue = _orderItems.Sum(p => p.CalculateValue());
            CalculateTotalDiscountValue();
        }
        public void CalculateTotalDiscountValue()
        {
            if (!UsedVoucher) return;

            decimal discount = 0;
            var value = TotalValue;

            if(Voucher.VoucherDiscountType == VoucherDiscountType.Percentage)
            {
                if(Voucher.Percent.HasValue) 
                { 
                    discount = (value * Voucher.Percent.Value) / 100;
                    value -= discount;
                }
            }
            else
            {
                if(Voucher.DiscountValue.HasValue)
                {
                    discount = Voucher.DiscountValue.Value;
                    value -= discount;
                }
            }

            TotalValue = value < 0 ? 0 : value;
            Discount = discount;
        }

        public bool OrderItemExists(OrderItem item)
        {
            return _orderItems.Any(p => p.ProductId == item.ProductId);
        }

        public void AddItem(OrderItem item)
        {
            if (!item.ItsValid()) return;

            item.AssociateOrder(Id);

            if (OrderItemExists(item))
            {
                var existingItem = _orderItems.FirstOrDefault(p => p.ProductId== item.ProductId);
                existingItem.AddUnits(item.Quantity);
                item = existingItem;

                _orderItems.Remove(existingItem);
            }

            item.CalculateValue();
            _orderItems.Add(item);

            CaculateOrderValue();
        }

        public void RemoveItem(OrderItem item)
        {
            if (!item.ItsValid()) return;

            var existingItem = _orderItems.FirstOrDefault(p => p.ProductId == item.ProductId);

            if (existingItem == null) throw new DomainException("This item doesn`t belong to this order");
            _orderItems.Remove(existingItem);

            CaculateOrderValue();
        }

        public void UpdateItem(OrderItem item)
        {
            if (!item.ItsValid()) return;
            item.AssociateOrder(Id);

            var existingItem = _orderItems.FirstOrDefault(p => p.ProductId == item.ProductId);

            if (existingItem == null) throw new DomainException("This item doesn`t belong to this order");

            _orderItems.Remove(existingItem);
            _orderItems.Add(item);

            CaculateOrderValue();
        }

        public void UpdateUnits(OrderItem item, int units)
        {
            item.UpdateUnits(units);
            UpdateItem(item);
        }

        public void BecomeDraft()
        {
            OrderStatus = OrderStatus.Draft;
        }

        public void StartOrder()
        {
            OrderStatus = OrderStatus.Started;
        }
        public void EndOrder()
        {
            OrderStatus = OrderStatus.Paid;
        }
        public void CancelOrder()
        {
            OrderStatus = OrderStatus.Cancelled;
        }

        public static class OrderFactory
        {
            public static Order NewOrderDraft(Guid clientId)
            {
                var order = new Order
                {
                    ClientId = clientId,
                };

                order.BecomeDraft();
                return order;
            }
        }
    }

}
