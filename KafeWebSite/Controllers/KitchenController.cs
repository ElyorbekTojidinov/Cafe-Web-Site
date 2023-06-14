using Microsoft.AspNetCore.Mvc;

namespace KafeWebSite.Controllers
{
    public class KitchenController : Controller
    {
        private readonly ILogger<KitchenController> _logger;

        public KitchenController(ILogger<KitchenController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Menu()
        {
            return View();
        }
        public IActionResult News()
        {
            return View();
        }
        public IActionResult News_detail()
        {
            return View();
        }
    }
}
