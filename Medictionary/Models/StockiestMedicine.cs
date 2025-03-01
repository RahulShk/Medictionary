using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medictionary.Models
{
    public class StockiestMedicine
    {
        [Key, Column(Order = 0)]
        public string StockiestID { get; set; }

        [ForeignKey("StockiestID")]
        public User Stockiest { get; set; }

        [Key, Column(Order = 1)]
        public string MedicineID { get; set; }

        [ForeignKey("MedicineID")]
        public Medicine Medicine { get; set; }

        public int Quantity { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; } = DateTime.UtcNow;
        public string? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}