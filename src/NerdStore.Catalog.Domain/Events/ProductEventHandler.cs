using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Catalog.Domain.Events
{
    public class ProductEventHandler : INotificationHandler<ProductBelowInventoryEvent>
    {
        private readonly IProductRepository _productRepository;

        public ProductEventHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Handle(ProductBelowInventoryEvent message, CancellationToken cancellation)
        {
            var product = await _productRepository.GetById(message.AggregateId);

            // Enviar um email para aquisição demais produtos.
        }
    }
}
