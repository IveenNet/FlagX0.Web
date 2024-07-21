using FlagX0.Web.Application.DTO;
using FlagX0.Web.Transversal.Common;

namespace FlagX0.Web.Models.ViewModels
{
    public class FlagIndexViewModel
    {

        //public List<FlagDto> Flags { get; set; }

        public Pagination<FlagDto> Pagination { get; set; }
        public List<int> SelectOption { get; set; } = [5, 10, 15];

    }
}
