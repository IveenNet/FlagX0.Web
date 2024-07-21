using FlagX0.Web.Application.Interface.UseCases;
using FlagX0.Web.Core.Entities;
using FlagX0.Web.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ROP;

namespace FlagX0.Web.Application.UseCases.Flags
{
    public class DeleteFlagApplication(ApplicationDbContext _applicationDbContext,  IFlagUserDetails _flagUserDetails) : IDeleteFlagApplication
    {
        public async Task<Result<bool>> Execute(string flagName) => await GetEntity(flagName).Bind(DeleteEntity);

        private async Task<Result<FlagEntity>> GetEntity(string flagName) => await _applicationDbContext.Flags
                .Where(a => a.UserId == _flagUserDetails.UserId && a.Name.ToLower() == flagName)
                .SingleAsync();

        private async Task<Result<bool>> DeleteEntity(FlagEntity entity)
        {
            /*
             * Esto es un Hard delete, pero si luego queremos recuperarlo no podriamos
             * 
            _applicationDbContext.Flags.Remove(entity);
            await _applicationDbContext.SaveChangesAsync();
            */

            entity.IsDeleted = true;
            entity.DeleteTimeUtc = DateTime.UtcNow;
            await _applicationDbContext.SaveChangesAsync();

            return true;
        }
    }
}
