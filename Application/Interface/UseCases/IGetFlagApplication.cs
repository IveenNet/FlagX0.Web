using FlagX0.Web.Application.DTO;
using ROP;

namespace FlagX0.Web.Application.Interface.UseCases
{
    public interface IGetFlagApplication
    {
        public Task<Result<List<FlagDto>>> Execute();
        public Task<Result<FlagDto>> Execute(string flagName);
    }
}
