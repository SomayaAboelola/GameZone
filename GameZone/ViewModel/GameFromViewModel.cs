using GameZone.Attribute;
using GameZone.Settings;
using System.ComponentModel.DataAnnotations;

namespace GameZone.ViewModel
{
    public class GameFromViewModel
    {
        [MaxLength(250)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(2500)]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [Display(Name = "Support Device")]
        public List<int> SelectedDevices { get; set; } = default!;
        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Devices { get; set; } = Enumerable.Empty<SelectListItem>();
     
      
    }
}
