using static NerdStore.Core.Data.IRepository;

namespace NerdStore.Sales.Domain
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> GetById(Guid Id);
        Task<IEnumerable<Order>> GetListByClientId(Guid clientId);
        Task<Order> GetOrderDraftByClientId(Guid clientId);
        void Add(Order order);
        void Update(Order order);

        Task<OrderItem> GetItemById(Guid id);
        Task<OrderItem> GetItemByOrder(Guid orderId, Guid productId);
        void AddItem(OrderItem orderItem);
        void UpdateItem(OrderItem orderItem);
        void RemoveItem(OrderItem orderItem);

        Task<Voucher> GetVoucherByCode(string code);
    }

}
