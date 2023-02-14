using Microsoft.EntityFrameworkCore;
using NerdStore.Catalog.Domain;
using NerdStore.Core.Data;

namespace NerdStore.Catalog.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogContext _context;

        public ProductRepository(CatalogContext context)
        {
            _context = context;
        }
        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.AsNoTracking().ToListAsync();
        }

        public async Task<Product> GetById(Guid id)
        {
            return await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetByCategory(int codigo)
        {
            return await _context.Products.AsNoTracking().Include(p => p.Category).Where(c => c.Category.Code == codigo).ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Categories.AsNoTracking().ToListAsync();
        }

        public void Add(Product Product)
        {
            _context.Products.Add(Product);
        }

        public void Update(Product Product)
        {
            _context.Products.Update(Product);
        }

        public void Add(Category categoria)
        {
            _context.Categories.Add(categoria);
        }

        public void Update(Category categoria)
        {
            _context.Categories.Update(categoria);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}