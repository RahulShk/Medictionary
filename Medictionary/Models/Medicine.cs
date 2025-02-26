using System.ComponentModel.DataAnnotations.Schema;

namespace Medictionary.Models
{
    public class Medicine
    {
        public string MedicineID { get; set; } = Guid.NewGuid().ToString();
        [ForeignKey("Industry")]
        public string IndustryID { get; set; }
        public Industry Industry { get; set; }
        public Image? MedicineImage { get; set; }
        public string Name { get; set; }
        public string Composition { get; set; }
        public string Manufacturer { get; set; }
        public string Batch { get; set; }
        public string ManufacturingDate { get; set; }
        public string ExpiryDate { get; set; }
        public decimal Price { get; set; }
        public string Stock { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; } = DateTime.UtcNow;
        public string? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}