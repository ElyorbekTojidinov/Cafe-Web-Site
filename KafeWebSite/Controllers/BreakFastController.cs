using Application.Common.Models;
using Application.UseCases.BreakFasts.Commands;
using Application.UseCases.BreakFasts.Queries;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Hosting;

namespace KafeWebSite.Controllers
{

    public class BreakFastController : BaseController
    {
        [HttpGet]
        public async ValueTask<IActionResult> GetAllBreakFast()
        {
          var breakFast =  _mediatr.Send(new GetAllBreakFastQuery());    
            return View(breakFast);
        }

        [HttpGet]
        [EnableRateLimiting("TokenBucketLimiter")]
        public async ValueTask<IActionResult> CreateBreakFast()
        {
            return View();
        }

        [HttpPost]
        public async ValueTask<IActionResult> CreateBreakFast(CreateBreakeFastCommand createBreakeFast)
        {
           var breakFast = await _mediatr.Send(new CreateBreakeFastCommand());
            return RedirectToAction("ViewBreakFast", breakFast);
        }

        [HttpGet]
        public async ValueTask<IActionResult> UpdateBreakFast()
        {
            return View();
        }

        [HttpPost]
        public async ValueTask<IActionResult> UpdateBreakFast(UpdateBreakFastCommand updateBreakFast)
        {
          var breakFast =   await _mediatr.Send(new UpdateBreakFastCommand());
            return RedirectToAction("ViewBreakFast", breakFast);
        }

        [HttpGet]
        public async ValueTask<IActionResult> DeleteBreakFast()
        {
            return View();
        }

        public async ValueTask<IActionResult> DeleteBreakFast(DeleteBreakFastCommand deleteBreakFast)
        {
            await _mediatr.Send(new DeleteBreakFastCommand());
            return RedirectToAction(nameof(GetAllBreakFast));
        }

        public async ValueTask<IActionResult> ViewBreakFast(Guid id)
        {
           BreakFastGetDto breakFast = await _mediatr.Send(new GetByIdBreakFastQuery());
            return View("ViewBreakFast", breakFast);
        }
        
    }
}
