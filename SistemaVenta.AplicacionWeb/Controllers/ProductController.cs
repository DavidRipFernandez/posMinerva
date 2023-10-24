using Microsoft.AspNetCore.Mvc;

namespace SistemaVenta.AplicacionWeb.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View("ProductIndex");
        }
    }
}
