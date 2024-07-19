using FlagX0.Web.Application.Interface.UseCases;
using Microsoft.AspNetCore.Identity;

namespace FlagX0.Web.Application.UseCases.Flags
{
    public class FlagUserDetails(IHttpContextAccessor _httpContextAccessor, UserManager<IdentityUser> _userManager) : IFlagUserDetails
    {

        public string UserId => 
            _userManager.GetUserId(_httpContextAccessor.HttpContext.User) ?? throw new Exception("This workflow require authentication");

    }
}
