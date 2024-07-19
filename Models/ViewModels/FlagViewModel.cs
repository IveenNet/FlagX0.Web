using System.ComponentModel.DataAnnotations;

namespace FlagX0.Web.Models.ViewModels
{
    public class FlagViewModel
    {
        [Required]
        public string Error { get; set; }
        public string Name { get; set; }
        public bool IsEnabled { get; set; }

    }
}
