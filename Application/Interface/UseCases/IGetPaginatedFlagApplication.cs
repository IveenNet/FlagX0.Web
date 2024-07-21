using FlagX0.Web.Application.DTO;
using FlagX0.Web.Transversal.Common;
using ROP;

namespace FlagX0.Web.Application.Interface.UseCases
{
    public interface IGetPaginatedFlagApplication
    {
        public Task<Result<Pagination<FlagDto>>> Execute(string? search, int page, int pageSize);
    }
}
