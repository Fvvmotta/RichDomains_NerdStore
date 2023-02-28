using Microsoft.AspNetCore.Mvc;
using NerdStore.Sales.Application.Queries;

namespace NerdStore.WebApp.MVC.Extensions
{
    public class CartViewComponent : ViewComponent
    {
        private readonly IOrderQueries _orderQueries;

        //TODO: Get Logged In Client
        protected Guid ClientId = Guid.Parse("ca72135c-6b2b-478a-83e4-9d48c35f4cc4");

        public CartViewComponent(IOrderQueries orderQueries)
        {
            _orderQueries = orderQueries;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cart = await _orderQueries.GetClientCart(ClientId);
            var itens = cart?.Items.Count ?? 0;

            return View(itens);
        }
    }
}
