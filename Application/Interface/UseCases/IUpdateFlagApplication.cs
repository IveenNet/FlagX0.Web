using FlagX0.Web.Application.DTO;
using ROP;

namespace FlagX0.Web.Application.Interface.UseCases
{
    public interface IUpdateFlagApplication
    {

        Task<Result<FlagDto>> Execute(FlagDto flagDto);

    }
}
