using Microsoft.AspNetCore.Mvc;

namespace SistemaVenta.AplicacionWeb.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View("CategoryIndex");
        }
    }
}
