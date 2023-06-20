using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KafeWebSite.Controllers
{
    [Authorize(Roles = "admin")]
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
