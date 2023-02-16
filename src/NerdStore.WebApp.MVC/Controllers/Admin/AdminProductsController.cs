using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalog.Application.Services;
using NerdStore.Catalog.Application.ViewModels;
using System.Diagnostics.CodeAnalysis;

namespace NerdStore.WebApp.MVC.Controllers.Admin
{
    public class AdminProductsController : Controller
    {
        private readonly IProductAppService _productAppService;

        public AdminProductsController(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        [HttpGet]
        [Route("admin-product")]
        public async Task<IActionResult> Index()
        {
            return View(await _productAppService.GetAll());
        }

        [Route("new-product")]
        public async Task<IActionResult> NewProduct()
        {
            return View(await PopulateCategories(new ProductViewModel()));
        }

        [HttpPost]
        [Route("new-product")]
        public async Task<IActionResult> NewProduct(ProductViewModel productViewModel)
        {
            if(!ModelState.IsValid) return View(await PopulateCategories(productViewModel));

            await _productAppService.AddProduct(productViewModel);

            return RedirectToAction("Index");
        }
        [HttpGet]
        [Route("edit-product")]
        public async Task<IActionResult> UpdateProduct(Guid id)
        {
            return View(await PopulateCategories(await _productAppService.GetById(id)));
        }

        [HttpPost]
        [Route("edit-product")]
        public async Task<IActionResult> UpdateProduct(Guid id, ProductViewModel produtoViewModel)
        {
            var produto = await _productAppService.GetById(id);
            produtoViewModel.InventoryQuantity = produto.InventoryQuantity;

            ModelState.Remove("QuantidadeEstoque");
            if (!ModelState.IsValid) return View(await PopulateCategories(produtoViewModel));

            await _productAppService.UpdateProduct(produtoViewModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("products-update-inventory")]
        public async Task<IActionResult> UpdateInventory(Guid id)
        {
            return View("Inventory", await _productAppService.GetById(id));
        }

        [HttpPost]
        [Route("products-update-inventory")]
        public async Task<IActionResult> UpdateInventory(Guid id, int quantity)
        {
            if (quantity > 0)
            {
                await _productAppService.ReplenishInventory(id, quantity);
            }
            else
            {
                await _productAppService.DebitInventory(id, quantity);
            }

            return View("Index", await _productAppService.GetAll());
        }
        private async Task<ProductViewModel> PopulateCategories(ProductViewModel product)
        {
            product.Categories = await _productAppService.GetCategories();
            return product;
        }
    }
}
