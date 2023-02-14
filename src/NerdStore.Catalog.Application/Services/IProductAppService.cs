using NerdStore.Catalog.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Catalog.Application.Services
{
    public interface IProductAppService : IDisposable
    {
        Task<IEnumerable<ProductViewModel>> GetByCategory(int code);
        Task<ProductViewModel> GetById(Guid id);
        Task<IEnumerable<ProductViewModel>> GetAll();
        Task<IEnumerable<CategoryViewModel>> GetCategories();

        Task AddProduct(ProductViewModel productViewModel);
        Task UpdateProduct(ProductViewModel productViewModel);

        Task<ProductViewModel> DebitInventory(Guid id, int quantity);
        Task<ProductViewModel> ReplenishInventory(Guid id, int quantity);

    }
}
