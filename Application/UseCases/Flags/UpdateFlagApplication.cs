using FlagX0.Web.Application.DTO;
using FlagX0.Web.Application.Interface.UseCases;
using FlagX0.Web.Core.Entities;
using FlagX0.Web.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ROP;
using System.Threading.Tasks;

namespace FlagX0.Web.Application.UseCases.Flags;

public class UpdateFlagApplication(ApplicationDbContext _applicationDbContext) : IUpdateFlagApplication
{
    public async Task<Result<FlagDto>> Execute(FlagDto flagDto) => await VerifyIsTheOnlyOneWithThatName(flagDto)
        .Bind(x => GetFromDb(x.Id))
        .Bind(x => Update(x, flagDto))
        .Map(x => x.ToDto());

    private async Task<Result<FlagDto>> VerifyIsTheOnlyOneWithThatName(FlagDto dto)
    {
        var normalizedDtoName = dto.Name.ToLower();
        bool alreadyExist = await _applicationDbContext.Flags
            .AnyAsync(a => a.Name.ToLower() == normalizedDtoName && a.Id != dto.Id);

        if (alreadyExist) return Result.Failure<FlagDto>("Flag with the same name already exists");

        return dto;
    }

    private async Task<Result<FlagEntity>> GetFromDb(int id)
    {
        var flag = await _applicationDbContext.Flags
            .Where(a => a.Id == id)
            .SingleOrDefaultAsync();

        if (flag == null)
        {
            return Result.Failure<FlagEntity>("Flag not found");
        }

        return Result.Success(flag);
    }

    private async Task<Result<FlagEntity>> Update(FlagEntity entity, FlagDto flagDto)
    {
        entity.Value = flagDto.IsEnabled;
        entity.Name = flagDto.Name;
        await _applicationDbContext.SaveChangesAsync();

        return Result.Success(entity);
    }
}
