using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Medictionary.Models
{
    public interface IDocument
    {
        string ID { get; set; }
        DateTime CreatedDate { get; set; }
        string? CreatedBy { get; set; }
        DateTime LastUpdatedDate { get; set; }
        string? LastUpdatedBy { get; set; }
        bool IsDeleted { get; set; }
    }

    [Table("Documents")] 
    public class Document : IDocument
    {
        [Key]
        [Column("id")] 
        [Required]
        public string ID { get; set; } = Guid.NewGuid().ToString();

        [Column("created_date")] 
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Column("created_by")] 
        public string? CreatedBy { get; set; }

        [Column("last_updated_date")]
        public DateTime LastUpdatedDate { get; set; }

        [Column("last_updated_by")] 
        public string? LastUpdatedBy { get; set; }

        [Column("is_deleted")]
        [Required]
        public bool IsDeleted { get; set; } = false;
    }
}
