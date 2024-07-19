using Microsoft.AspNetCore.Identity;

namespace FlagX0.Web.Application.DTO
{
    /*public class FlagDto
    {

        public string Name { get; set; }
        public bool IsEnabled { get; set; }

    }*/
    
    //Primary constructors
    public record class FlagDto(string Name, bool IsEnabled, int Id);
}
