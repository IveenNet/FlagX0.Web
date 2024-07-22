using FlagX0.Web.Application.DTO;
using FlagX0.Web.Application.Interface.UseCases;
using FlagX0.Web.Application.UseCases.Flags;
using FlagX0.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ROP;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace FlagX0.Web.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class FlagsController : Controller
    {
        /*
        private readonly ICreateFlagApplication _flagApplication;
        private readonly IGetFlagApplication _getFlagApplication;
        private readonly IUpdateFlagApplication _updateFlagApplication;
        private readonly IDeleteFlagApplication _deleteFlagApplication;
        private readonly IGetPaginatedFlagApplication _getPaginatedFlagApplication;

        public FlagsController(ICreateFlagApplication flagApplication, IGetFlagApplication getFlagApplication, IUpdateFlagApplication updateFlagApplication, IDeleteFlagApplication deleteFlagApplication, IGetPaginatedFlagApplication getPaginatedFlagApplication)
        {
            _flagApplication = flagApplication;
            _getFlagApplication = getFlagApplication;
            _updateFlagApplication = updateFlagApplication;
            _deleteFlagApplication = deleteFlagApplication;
            _getPaginatedFlagApplication = getPaginatedFlagApplication;
        }
                [HttpGet("")]
        [HttpGet("(page:int)")]
        public async Task<IActionResult> Index(string)
        {
            var result = await _getFlagApplication.Execute();
            if (result.Success)
            {
                return View(new FlagIndexViewModel() { Flags = result.Value });
            }

            // Manejo de errores, redirigir o mostrar un mensaje
            ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault()?.Message ?? "An error occurred.");
            return View(new FlagIndexViewModel() { Flags = new List<FlagDto>() });
        }*/

        private readonly FlagsApplication _flagsApplication;

        public FlagsController(FlagsApplication flagsApplication) { _flagsApplication = flagsApplication; }

        [HttpGet("")]
        [HttpGet("(page:int)")]
        public async Task<IActionResult> Index(string? search,
             [Range(1, int.MaxValue, ErrorMessage = "page must be > 1")] int page = 1, int size = 5)
        {
            if (!ModelState.IsValid) page = 1;

            var listFlags = (await _flagsApplication.GetPaginated.Execute(search, page, size)).Throw();

            // Manejo de errores, redirigir o mostrar un mensaje
            return View(new FlagIndexViewModel() { Pagination = listFlags });
        }

        [HttpGet("{flagName}")]
        public async Task<IActionResult> GetSingle(string flagName, string? message = null)
        {
            // Añadir logging para ver si se llama al método
            Debug.WriteLine($"GetSingle llamado con flagName: {flagName}");

            var singleFlagResult = await _flagsApplication.Get.Execute(flagName);

            if (!singleFlagResult.Success)
            {
                // Manejo de errores, por ejemplo, redirigir a una página de error o mostrar un mensaje de error
                ModelState.AddModelError(string.Empty, singleFlagResult.Errors.FirstOrDefault()?.Message ?? "An error occurred.");
                return View("Error"); // Asegúrate de tener una vista de error configurada
            }

            var singleFlagViewModel = new SingleFlagViewModel(singleFlagResult.Value, message ?? string.Empty);
            return View("SingleFlag", singleFlagViewModel);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View(new FlagViewModel());
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(FlagViewModel request)
        {
            Result<bool> isCreated = await _flagsApplication.Add.Execute(request.Name, request.IsEnabled);

            if (ModelState.IsValid)
            {
                if (isCreated.Success)
                {
                    return RedirectToAction("Index", "Home");
                }
                // Manejo seguro de los errores
                if (isCreated.Errors.Any())
                {
                    ModelState.AddModelError(string.Empty, isCreated.Errors.First().Message);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An unknown error occurred.");
                }
            }

            return View(new FlagViewModel()
            {
                Error = isCreated.Errors.FirstOrDefault()?.Message,
                IsEnabled = request.IsEnabled,
                Name = request.Name
            });
        }

        [HttpPost("{flagName}")]
        public async Task<IActionResult> Update(FlagDto flag)
        {
            // Añadir logging para ver si se llama al método
            Debug.WriteLine($"Update llamado con flagName: {flag.Name}");

            var singleFlagResult = await _flagsApplication.Update.Execute(flag);

            if (!singleFlagResult.Success)
            {
                var singleFlagViewModel = new SingleFlagViewModel(flag, singleFlagResult.Errors.FirstOrDefault()?.Message ?? "An error occurred.");
                return View("SingleFlag", singleFlagViewModel);
            }

            return RedirectToAction("GetSingle", new { flagName = flag.Name, message = "Update successfully!" });
        }

        [HttpGet("delete/{flagName}")]
        public async Task<IActionResult> Delete(string flagName)
        {
            var isDeleted = await _flagsApplication.Delete.Execute(flagName);

            if (isDeleted.Success) return RedirectToAction("");

            return RedirectToAction("GetSingle", new { flagName = flagName, message = "Updated correctly" });
        }
    }
}
