using Application.Common.Models;
using Application.UseCases.BreakFasts.Commands;
using Application.UseCases.BreakFasts.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace KafeWebSite.Controllers
{
    [Authorize(Roles = "admin")]
    public class BreakFastController : BaseController
    {
        [HttpGet]
        [EnableRateLimiting("TokenBucketLimiter")]
        public async ValueTask<IActionResult> GetAllBreakFast()
        {
            IQueryable<BreakFastGetDto> breakFast = await _mediatr.Send(new GetAllBreakFastQuery());
            return View(breakFast);
        }

        [HttpGet]
        public async ValueTask<IActionResult> CreateBreakFast()
        {
            return View();
        }

        [HttpPost]
        public async ValueTask<IActionResult> CreateBreakFast([FromForm] CreateBreakeFastCommand createBreakeFast)
        {
            var breakFast = await _mediatr.Send(createBreakeFast);
            BreakFastGetDto breakFast1 = await _mediatr.Send(new GetByIdBreakFastQuery() { Id = breakFast });
            return View("ViewBreakFast", breakFast1);
        }

        [HttpGet]
        public async ValueTask<IActionResult> UpdateBreakFast(Guid id)
        {
            BreakFastGetDto updateBreakFast = await _mediatr.Send(new GetByIdBreakFastQuery() { Id = id });
            UpdateBreakFastCommand command = new UpdateBreakFastCommand()
            {
                Id = updateBreakFast.Id,
                Name = updateBreakFast.Name,
                ImgFileName = updateBreakFast.ImgFileName,
                Price = updateBreakFast.Price,
                Rewievs = updateBreakFast.Rewievs,
                Quality = updateBreakFast.Quality
            };
            return View(command);
        }

        [HttpPost]
        public async ValueTask<IActionResult> UpdateBreakFast([FromForm] UpdateBreakFastCommand updateBreakFast)
        {

            var breakFast = await _mediatr.Send(updateBreakFast);
            BreakFastGetDto breakFastGet = await _mediatr.Send(new GetByIdBreakFastQuery() { Id = breakFast});
            return RedirectToAction("ViewBreakFast", breakFastGet);
        }

      
        public async ValueTask<IActionResult> DeleteBreakFast(Guid id)
        {
            BreakFastGetDto deleteBreakFast = await _mediatr.Send(new GetByIdBreakFastQuery() { Id = id });
            DeleteBreakFastCommand command = new DeleteBreakFastCommand()
            {
                Id = deleteBreakFast.Id,
            };
            var res = await _mediatr.Send(command);
            return RedirectToAction("GetAllBreakFast");
        }

        public async ValueTask<IActionResult> ViewBreakFast(Guid id)
        {
            BreakFastGetDto breakFast = await _mediatr.Send(new GetByIdBreakFastQuery() { Id = id });
            //return View(breakFast);
            return View("ViewBreakFast", breakFast);
        }

    }
}
