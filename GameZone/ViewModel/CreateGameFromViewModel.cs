using GameZone.Attribute;
using GameZone.Entities;
using GameZone.Settings;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GameZone.ViewModel
{
    public class CreateGameFromViewModel :GameFromViewModel
    {
      
        [AllowedExtension(FileSetting.AllowedExtensions),
           MaxFileSize(FileSetting.MaxFileSizeInBytes)]
        public IFormFile Cover { get; set; } = default!;

    }
}
