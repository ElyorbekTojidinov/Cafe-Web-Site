using Microsoft.AspNetCore.Mvc;

namespace KafeWebSite.Controllers
{

    public class AdminPageController : Controller
    {
        private readonly ILogger<KitchenController> _logger;

        public AdminPageController(ILogger<KitchenController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
