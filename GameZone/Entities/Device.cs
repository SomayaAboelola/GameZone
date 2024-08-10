using System.ComponentModel.DataAnnotations;

namespace GameZone.Entities
{
    public class Device : BaseEntity
    {
        [MaxLength(100)]
        public string Icon { get; set; } = string.Empty;
        public ICollection<GameDevice> Devices { get; set; } = new List<GameDevice>();

    }
}
