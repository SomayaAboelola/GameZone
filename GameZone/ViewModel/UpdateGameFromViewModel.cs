
using GameZone.Attribute;
using GameZone.Settings;

namespace GameZone.ViewModel
{
    public class UpdateGameFromViewModel :GameFromViewModel
    {
        public int Id { get; set; }
        [AllowedExtension(FileSetting.AllowedExtensions),
        MaxFileSize(FileSetting.MaxFileSizeInBytes)]
        public IFormFile? Cover { get; set; } = default!;
        public string? UrlCover { get; set; } = default!;   
    }
}
