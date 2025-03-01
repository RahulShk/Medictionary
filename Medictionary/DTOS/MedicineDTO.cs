using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Medictionary.DTOS
{
    public class MedicineDTO
    {
        [Required]
        public string IndustryID { get; set; }

        public IFormFile? MedicineImageFile { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Composition { get; set; }

        [Required]
        public string Manufacturer { get; set; }

        [Required]
        public string Batch { get; set; }

        [Required]
        public string ManufacturingDate { get; set; }

        [Required]
        public string ExpiryDate { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a valid price")]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative")]
        public int Stock { get; set; }
    }
}