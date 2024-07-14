using FlagX0.Web.Application.Interface.UseCases;
using FlagX0.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlagX0.Web.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class FlagsController : Controller
    {
        private readonly IFlagApplication _flagApplication;

        public FlagsController(IFlagApplication flagApplication)
        {
            _flagApplication = flagApplication;
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View(new FlagViewModel());
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(FlagViewModel request)
        {
            if (ModelState.IsValid)
            {
                bool isCreated = await _flagApplication.Execute(request.Name, request.IsEnabled);
                if (isCreated)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "An error occurred while creating the flag.");
            }
            return View(request);
        }
    }
}
