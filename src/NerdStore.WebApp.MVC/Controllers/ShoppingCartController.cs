using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalog.Application.Services;
using NerdStore.Core.Bus;
using NerdStore.Sales.Application.Commands;

namespace NerdStore.WebApp.MVC.Controllers
{
    public class ShoppingCartController : ControllerBase
    {

        private readonly IProductAppService _productAppService;
        private readonly IMediatorHandler _mediatorHandler;
        public ShoppingCartController(IProductAppService productAppService, 
                                      IMediatorHandler mediatorHandler)
        {
            _productAppService = productAppService;
            _mediatorHandler = mediatorHandler;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("my-cart")]
        public async Task<IActionResult> AddItem(Guid id, int quantity)
        {
            var product = await _productAppService.GetById(id);
            if (product == null) return BadRequest();

            if(product.InventoryQuantity < quantity)
            {
                TempData["Error"] = "Product with insuficient inventory";
                return RedirectToAction("ProductDetail", "Display", new { id });
            }

            var command = new AddItemToOrderCommand(ClientId, product.Id, product.Name, quantity, product.Value);
            await _mediatorHandler.SendCommand(command);

            // if everything ok

            // if not ok
            TempData["Error"] = "Product Unavailable";
            return RedirectToAction("ProductDetail", "Display", new { id });
        }
    }
}
