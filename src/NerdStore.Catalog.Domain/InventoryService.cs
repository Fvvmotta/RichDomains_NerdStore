using NerdStore.Catalog.Domain.Events;
using NerdStore.Core.Bus;

namespace NerdStore.Catalog.Domain
{
    public class InventoryService : IInventoryService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMediatorHandler _bus;

        public InventoryService(IProductRepository productRepository,
                                IMediatorHandler bus)
        {
            _productRepository = productRepository;
            _bus = bus;
        }

        public async Task<bool> DebitInventory(Guid productId, int quantity)
        {
            var product = await _productRepository.GetById(productId);

            if (product == null) return false;

            if (!product.HasInventory(quantity)) return false;

            product.DebitInventory(quantity);

            // TODO: Parametrizar a quantidade de estoque baixo
            if (product.InventoryQuantity < 10)
            {
                await _bus.PublishEvent(new ProductBelowInventoryEvent(product.Id, product.InventoryQuantity));
            }

            _productRepository.Update(product);
            return await _productRepository.UnitOfWork.Commit();
        }

        public async Task<bool> ReplenishInventory(Guid productId, int quantity)
        {
            var product = await _productRepository.GetById(productId);

            if (product == null) return false;
            product.ReplenishInventory(quantity);

            _productRepository.Update(product);
            return await _productRepository.UnitOfWork.Commit();
        }

        public void Dispose()
        {
            _productRepository.Dispose();
        }
    }
}
