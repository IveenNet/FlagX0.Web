using FlagX0.Web.Application.DTO;
using FlagX0.Web.Application.Interface.UseCases;
using FlagX0.Web.Core.Entities;
using FlagX0.Web.Infrastructure.Data;
using FlagX0.Web.Transversal.Common;
using Microsoft.EntityFrameworkCore;
using ROP;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlagX0.Web.Application.UseCases.Flags
{
    public class GetPaginatedFlagApplication(ApplicationDbContext _applicationDbContext) : IGetPaginatedFlagApplication
    {
        public async Task<Result<Pagination<FlagDto>>> Execute(string? search, int page, int pageSize) => await ValidatePage(page)
            .Fallback(_ =>
            {
                page = 1;
                return Result.Unit;
            })
            .Bind(_ => ValidatePageSize(pageSize)
                .Fallback(_ =>
                {
                    pageSize = 5;
                    return Result.Unit;
                })
            ).Async()
            .Bind(x => GetFromDb(search, page, pageSize))
            .Map(x => x.ToDto())
            .Combine(x => TotalElements(search))
            .Map(x => new Pagination<FlagDto>(x.Item1, x.Item2, pageSize, page, search));

        private Result<Unit> ValidatePage(int page)
        {
            if (page < 1) return Result.Failure("page not supported");

            return Result.Unit;
        }

        private Result<Unit> ValidatePageSize(int pageSize)
        {
            int[] allowedValues = { 5, 10, 15 };

            if (!allowedValues.Contains(pageSize)) return Result.Failure("page size not supported");

            return Result.Unit;
        }

        private async Task<Result<List<FlagEntity>>> GetFromDb(string? search, int page, int pageSize)
        {
            var query = _applicationDbContext.Flags.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(a => a.Name.ToLower().Contains(search.ToLower()));
            }

            var pagedItems = await query
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .ToListAsync();

            return Result.Success(pagedItems);
        }

        private async Task<Result<int>> TotalElements(string? search)
        {
            var query = _applicationDbContext.Flags.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(a => a.Name.ToLower().Contains(search.ToLower()));
            }

            var count = await query.CountAsync();
            return Result.Success(count);
        }
    }
}
