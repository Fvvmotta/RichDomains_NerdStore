namespace NerdStore.Catalog.Domain
{
    public interface IInventoryService : IDisposable
    {
        Task<bool> DebitInventory(Guid productId, int quantity);
        Task<bool> ReplenishInventory(Guid productId, int quantity);
    }
}
