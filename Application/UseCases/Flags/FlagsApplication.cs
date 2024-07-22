using FlagX0.Web.Application.Interface.UseCases;

namespace FlagX0.Web.Application.UseCases.Flags
{
    public record class FlagsApplication(ICreateFlagApplication Add,
        IGetPaginatedFlagApplication GetPaginated,
        IGetFlagApplication Get,
        IUpdateFlagApplication Update,
        IDeleteFlagApplication Delete)
    { }

}
