using System.ComponentModel.DataAnnotations;

namespace Medictionary.Models
{
    public class Industry : Document
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        public string? Location { get; set; }
    }
}