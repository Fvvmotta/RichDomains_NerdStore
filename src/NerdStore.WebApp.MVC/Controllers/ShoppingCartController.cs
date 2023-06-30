using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalog.Application.Services;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.Sales.Application.Commands;
using NerdStore.Sales.Application.Queries;

namespace NerdStore.WebApp.MVC.Controllers
{
    public class ShoppingCartController : ControllerBase
    {

        private readonly IProductAppService _productAppService;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IOrderQueries _orderQueries;

        public ShoppingCartController(INotificationHandler<DomainNotification> notifications,
                                      IProductAppService productAppService, 
                                      IMediatorHandler mediatorHandler,
                                      IOrderQueries orderQueries) : base (notifications, mediatorHandler) 
        {
            _productAppService = productAppService;
            _mediatorHandler = mediatorHandler;
            _orderQueries = orderQueries;
        }

        [Route("my-cart")]
        public async Task<IActionResult> Index()
        {
            return View(await _orderQueries.GetClientCart(ClientId));
        }

        [HttpPost]
        [Route("add-item")]
        public async Task<IActionResult> AddItem(Guid id, int quantity)
        {
            var product = await _productAppService.GetById(id);
            if (product == null) return BadRequest();

            if(product.InventoryQuantity < quantity)
            {
                TempData["Error"] = "Product with insuficient inventory";
                return RedirectToAction("ProductDetail", "Display", new { id });
            }

            var command = new AddItemOrderCommand(ClientId, product.Id, product.Name, quantity, product.Value);
            await _mediatorHandler.SendCommand(command);

            if (OperationValid())
            {
                return RedirectToAction("Index");
            }

            TempData["Errors"] = GetErrorMessages();
            return RedirectToAction("ProductDetail", "Display", new { id });
        }

        [HttpPost]
        [Route("remove-item")]
        public async Task<IActionResult> RemoveItem(Guid id)
        {
            var product = await _productAppService.GetById(id);
            if (product == null) return BadRequest();

            var command = new RemoveItemOrderCommand(ClientId, id);
            await _mediatorHandler.SendCommand(command);

            if (OperationValid())
            {
                return RedirectToAction("Index");
            }

            return View("Index", await _orderQueries.GetClientCart(ClientId));
        }

        [HttpPost]
        [Route("update-item")]
        public async Task<IActionResult> UpdateItem(Guid id, int quantity)
        {
            var product = await _productAppService.GetById(id);
            if (product == null) return BadRequest();

            var command = new UpdateItemOrderCommand(ClientId, id, quantity);
            await _mediatorHandler.SendCommand(command);

            if (OperationValid())
            {
                return RedirectToAction("Index");
            }

            return View("Index", await _orderQueries.GetClientCart(ClientId));
        }

        [HttpPost]
        [Route("apply-Voucher")]
        public async Task<IActionResult> ApplyVoucher(string voucherCode)
        {
            var command = new ApplyVoucherOrderCommand(ClientId, voucherCode);
            await _mediatorHandler.SendCommand(command);

            if (OperationValid())
            {
                return RedirectToAction("Index");
            }

            return View("Index", await _orderQueries.GetClientCart(ClientId));
        }

    }
}
