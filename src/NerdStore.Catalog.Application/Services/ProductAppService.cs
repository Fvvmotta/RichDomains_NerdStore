using AutoMapper;
using NerdStore.Catalog.Application.ViewModels;
using NerdStore.Catalog.Domain;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Application.Services
{
    public class ProductAppService : IProductAppService
    {
        private readonly IProductRepository _productRepository;
        private readonly IInventoryService _inventoryService;
        private readonly IMapper _mapper;

        public ProductAppService(IProductRepository productRepository,
                                 IMapper mapper,
                                 IInventoryService inventoryService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _inventoryService = inventoryService;
        }

        public async Task<IEnumerable<ProductViewModel>> GetByCategory(int code)
        {
            return _mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetByCategory(code));
        }

        public async Task<ProductViewModel> GetById(Guid id)
        {
            return _mapper.Map<ProductViewModel>(await _productRepository.GetById(id));
        }

        public async Task<IEnumerable<ProductViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetAll());
        }

        public async Task<IEnumerable<CategoryViewModel>> GetCategories()
        {
            return _mapper.Map<IEnumerable<CategoryViewModel>>(await _productRepository.GetCategories());
        }

        public async Task AddProduct(ProductViewModel productViewModel)
        {
            var product = _mapper.Map<Product>(productViewModel);
            _productRepository.Add(product);

            await _productRepository.UnitOfWork.Commit();
        }

        public async Task UpdateProduct(ProductViewModel productViewModel)
        {
            var product = _mapper.Map<Product>(productViewModel);
            _productRepository.Update(product);

            await _productRepository.UnitOfWork.Commit();
        }

        public async Task<ProductViewModel> DebitInventory(Guid id, int quantity)
        {
            if (!_inventoryService.DebitInventory(id, quantity).Result)
            {
                throw new DomainException("Failed to debit inventory");
            }

            return _mapper.Map<ProductViewModel>(await _productRepository.GetById(id));
        }

        public async Task<ProductViewModel> ReplenishInventory(Guid id, int quantity)
        {
            if (!_inventoryService.ReplenishInventory(id, quantity).Result)
            {
                throw new DomainException("Failed to replenish inventory");
            }

            return _mapper.Map<ProductViewModel>(await _productRepository.GetById(id));
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
            _inventoryService?.Dispose();
        }

    }
}
