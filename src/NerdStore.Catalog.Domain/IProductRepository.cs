using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NerdStore.Core.Data.IRepository;

namespace NerdStore.Catalog.Domain
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(Guid id);
        Task<IEnumerable<Product>> GetByCategory(int Code);
        Task<IEnumerable<Category>> GetCategories();

        void Add(Product product);
        void Update(Product product);

        void Add(Category category);
        void Update(Category category);

    }
}
