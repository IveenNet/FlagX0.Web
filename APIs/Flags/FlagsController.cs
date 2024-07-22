using FlagX0.Web.Application.UseCases.Flags;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ROP;
using ROP.APIExtensions;

namespace FlagX0.Web.APIs.Flags
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class FlagsController(FlagsApplication flagsApplication) : ControllerBase
    {
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status404NotFound)]
        [HttpGet("{flagName}")]
        public async Task<IActionResult> GetSingleFlag(string flagName) => await flagsApplication
            .Get.Execute(flagName)
            .Map(a => a.IsEnabled)
            .ToActionResult();

    }
}
