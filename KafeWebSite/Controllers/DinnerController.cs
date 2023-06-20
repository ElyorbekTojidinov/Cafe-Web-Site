using Application.Common.Models;
using Application.UseCases.Dinners.Commands;
using Application.UseCases.Dinners.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace KafeWebSite.Controllers
{
    [Authorize(Roles = "admin")]
    public class DinnerController : BaseController
    {
        [HttpGet]
        [EnableRateLimiting("TokenBucketLimiter")]
        public async ValueTask<IActionResult> GetAllDinner()
        {
            IQueryable<DinnerGetDto> dinner = await _mediatr.Send(new GetAllDinnerQuery());
            return View(dinner);
        }

        [HttpGet]
        public async ValueTask<IActionResult> CreateDinner()
        {
            return View();
        }

        [HttpPost]
        public async ValueTask<IActionResult> CreateDinner([FromForm] CreateDinnerCommand createDinner)
        {
            var dinner = await _mediatr.Send(createDinner);
            DinnerGetDto resultDinner = await _mediatr.Send(new GetByIdDinnerQuery() { Id = dinner });
            return View("ViewDinner", resultDinner);
        }

        [HttpGet]
        public async ValueTask<IActionResult> UpdateDinner(Guid id)
        {
            DinnerGetDto dinner = await _mediatr.Send(new GetByIdDinnerQuery() { Id = id });
            UpdateDinnerCommand command = new UpdateDinnerCommand()
            {
                Id = dinner.Id,
                Name = dinner.Name,
                ImgFileName = dinner.ImgFileName,
                Price = dinner.Price,
                Rewievs = dinner.Rewievs,
                Quality = dinner.Quality
            };
            return View(command);
        }

        [HttpPost]
        public async ValueTask<IActionResult> UpdateDinner([FromForm] UpdateDinnerCommand updateDinner)
        {

            Guid dinner = await _mediatr.Send(updateDinner);
            DinnerGetDto resDinner = await _mediatr.Send(new GetByIdDinnerQuery() { Id = dinner });
            return View("ViewDinner", resDinner);
        }


        public async ValueTask<IActionResult> DeleteDinner(Guid id)
        {
            DinnerGetDto deleteDinner = await _mediatr.Send(new GetByIdDinnerQuery() { Id = id });
            DeleteDinnerCommand command = new DeleteDinnerCommand()
            {
                Id = deleteDinner.Id,
            };
            var res = await _mediatr.Send(command);
            return RedirectToAction("GetAllDinner");
        }

        public async ValueTask<IActionResult> ViewDinner(DinnerGetDto dinner)
        {
            return View("ViewDinner", dinner);
        }

    }
}
