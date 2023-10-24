using Microsoft.AspNetCore.Mvc;

namespace SistemaVenta.AplicacionWeb.Controllers
{
    public class BusinessController : Controller
    {
        public IActionResult Index()
        {
            return View("BusinessIndex");
        }
    }
}
