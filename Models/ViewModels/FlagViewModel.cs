using System.ComponentModel.DataAnnotations;

namespace FlagX0.Web.Models.ViewModels
{
    public class FlagViewModel
    {
        [Required]
        public string Name { get; set; }
        public bool IsEnabled { get; set; }

    }
}
