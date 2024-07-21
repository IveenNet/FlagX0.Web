using ROP;

namespace FlagX0.Web.Application.Interface.UseCases
{
    public interface IDeleteFlagApplication
    {
        public Task<Result<bool>> Execute(string flagName);
    }
}
