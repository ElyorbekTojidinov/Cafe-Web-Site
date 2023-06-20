using Application.Common.Models;
using Application.UseCases.Lunchs.Command;
using Application.UseCases.Lunchs.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace KafeWebSite.Controllers
{
    [Authorize(Roles = "admin")]
    public class LunchController : BaseController
    {

        [HttpGet]
        [EnableRateLimiting("TokenBucketLimiter")]
        public async ValueTask<IActionResult> GetAllLunch()
        {
            IQueryable<LunchGetDto> lunch = await _mediatr.Send(new GetAllLunchQuery());
            return View(lunch);
        }

        [HttpGet]
        public async ValueTask<IActionResult> CreateLunch()
        {
            return View();
        }

        [HttpPost]
        public async ValueTask<IActionResult> CreateLunch([FromForm] CreateLunchCommand createLunch)
        {
            var lunch = await _mediatr.Send(createLunch);
            LunchGetDto resultLunch = await _mediatr.Send(new GetByIdLunchQuery() { Id = lunch });
            return View("ViewLunch", resultLunch);
        }

        [HttpGet]
        public async ValueTask<IActionResult> UpdateLunch(Guid id)
        {
            LunchGetDto lunch = await _mediatr.Send(new GetByIdLunchQuery() { Id = id });
            UpdateLunchCammand command = new UpdateLunchCammand()
            {
                Id = lunch.Id,
                Name = lunch.Name,
                ImgFileName = lunch.ImgFileName,
                Price = lunch.Price,
                Rewievs = lunch.Rewievs,
                Quality = lunch.Quality
            };
            return View(command);
        }

        [HttpPost]
        public async ValueTask<IActionResult> UpdateLunch([FromForm] UpdateLunchCammand updateLunch)
        {

            Guid lunch = await _mediatr.Send(updateLunch);
            LunchGetDto resDinner = await _mediatr.Send(new GetByIdLunchQuery() { Id = lunch });
            return View("ViewLunch", resDinner);
        }


        public async ValueTask<IActionResult> DeleteLunch(Guid id)
        {
            LunchGetDto deleteLunch = await _mediatr.Send(new GetByIdLunchQuery() { Id = id });
            DeleteLunchCommand command = new DeleteLunchCommand()
            {
                Id = deleteLunch.Id,
            };
            var res = await _mediatr.Send(command);
            return RedirectToAction("GetAllLunch");
        }

        public async ValueTask<IActionResult> ViewLunch(DinnerGetDto dinner)
        {
            return View("ViewLunch", dinner);
        }


    }
}
