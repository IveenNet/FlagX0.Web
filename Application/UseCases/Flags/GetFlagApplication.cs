using FlagX0.Web.Application.DTO;
using FlagX0.Web.Application.Interface.UseCases;
using FlagX0.Web.Core.Entities;
using FlagX0.Web.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ROP;

namespace FlagX0.Web.Application.UseCases.Flags
{
    public class GetFlagApplication(ApplicationDbContext _applicationDbContext, IFlagUserDetails _flagUserDetails) : IGetFlagApplication
    {

        async Task<Result<List<FlagDto>>> IGetFlagApplication.Execute()
        {
            var response = await _applicationDbContext.Flags.Where(a => a.UserId == _flagUserDetails.UserId).AsNoTracking().ToListAsync();
            return response.Select(a => new FlagDto(a.Name, a.Value, a.Id)).ToList();
        }

        async Task<Result<FlagDto>> IGetFlagApplication.Execute(string flagName) => await GetFromDb(flagName).Map(x => x.ToDto());

        private async Task<Result<FlagEntity>> GetFromDb(string flagName)
        {
            var normalizedFlagName = flagName.ToLower();
            var flag = await _applicationDbContext.Flags
                .Where(a => a.UserId == _flagUserDetails.UserId && a.Name.ToLower() == normalizedFlagName)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            if (flag == null)
            {
                return Result.Failure<FlagEntity>("Flag not found");
            }

            return Result.Success(flag);
        }

    }
}
