using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalog.Application.Services;


namespace NerdStore.WebApp.MVC.Controllers
{
    public class DisplayController : Controller
    {
        private readonly IProductAppService _productAppService;

        public DisplayController(IProductAppService produtoAppService)
        {
            _productAppService = produtoAppService;
        }

        [HttpGet]
        [Route("")]
        [Route("catalog")]
        public async Task<IActionResult> Index()
        {
            return View(await _productAppService.GetAll());
        }

        [HttpGet]
        [Route("product-detail/{id}")]
        public async Task<IActionResult> ProductDetail(Guid id)
        {
            return View(await _productAppService.GetById(id));
        }
    }
}
