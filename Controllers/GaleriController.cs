using Microsoft.AspNetCore.Mvc;

namespace muglatanitim.Controllers
{
    public class GaleriController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
