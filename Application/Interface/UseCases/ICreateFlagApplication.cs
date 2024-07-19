using ROP;

namespace FlagX0.Web.Application.Interface.UseCases
{
    public interface ICreateFlagApplication
    {
        Task<Result<bool>> Execute(string flagName, bool isActive);
    }
}
