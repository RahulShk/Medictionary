using System.ComponentModel.DataAnnotations;

namespace Medictionary.Models
{
    public class Industry
    {
        public string IndustryId { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; } = DateTime.UtcNow;
        public string? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
        public ICollection<Medicine> Medicines { get; set; }
    }
}