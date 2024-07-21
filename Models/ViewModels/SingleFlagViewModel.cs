using FlagX0.Web.Application.DTO;

namespace FlagX0.Web.Models.ViewModels
{
    public record class SingleFlagViewModel(FlagDto Flag, string? Message)
    {
    }
    /*public record SingleFlagViewModel()
    {

        public FlagDto Flag { get; set; }
        public string? Message { get; set; }

    }*/

}
