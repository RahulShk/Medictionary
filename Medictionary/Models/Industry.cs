using System.ComponentModel.DataAnnotations;

namespace Medictionary.Models
{
    public class Industry : Document
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
    }
}