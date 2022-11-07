using System.ComponentModel.DataAnnotations;

namespace CW_0511.Classes
{
    public class Room
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Bg_image { get; set; }
        public string? Info { get; set; }
    }
}
