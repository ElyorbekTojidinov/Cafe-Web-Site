using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KafeWebSite.Controllers
{

    //[Route("[controller]")]
    //[ApiController]
    //[Authorize]
    public class BaseController : Controller
    {
        protected IMediator _mediatr => HttpContext.RequestServices.GetRequiredService<IMediator>();
    }
}
