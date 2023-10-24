using Microsoft.AspNetCore.Mvc;

namespace SistemaVenta.AplicacionWeb.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            return View("ReportIndex");
        }
    }
}
