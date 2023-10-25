using Microsoft.AspNetCore.Mvc;

namespace SistemaVenta.AplicacionWeb.Controllers
{
    public class SalesController : Controller
    {
        public IActionResult NewSales()
        {
            return View();
        }

        public IActionResult SalesHistory()
        {
            return View();
        }
    }
}
