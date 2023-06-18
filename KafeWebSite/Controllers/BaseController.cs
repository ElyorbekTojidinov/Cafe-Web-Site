using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KafeWebSite.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class BaseController : Controller
    {
        protected IMediator _mediatr => HttpContext.RequestServices.GetRequiredService<IMediator>();
    }
}
