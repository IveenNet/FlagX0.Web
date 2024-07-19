using FlagX0.Web.Core.Entities;

namespace FlagX0.Web.Application.DTO
{
    public static class FlagEntityExtension
    {

        public static FlagDto ToDto(this FlagEntity entity) => new FlagDto(entity.Name, entity.Value, entity.Id);

        public static List<FlagDto> ToDto(this List<FlagEntity> entities) => entities.Select(ToDto).ToList();


    }
}
