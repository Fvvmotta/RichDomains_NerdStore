using Microsoft.AspNetCore.Mvc;

namespace NerdStore.WebApp.MVC.Controllers
{
    public abstract class ControllerBase : Controller
    {
        protected Guid ClientId = Guid.Parse("ca72135c-6b2b-478a-83e4-9d48c35f4cc4");
    }
}
