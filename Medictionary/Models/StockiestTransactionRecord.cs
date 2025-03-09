using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medictionary.Models
{
    public class StockiestTransactionRecord
    {
        [Key]
        public int TransactionID { get; set; }
        public string StockiestID { get; set; }

        [ForeignKey("StockiestID")]
        public User Stockiest { get; set; }
        public string MedicineID { get; set; }

        [ForeignKey("MedicineID")]
        public Medicine Medicine { get; set; }
        public int Quantity { get; set; }
        public decimal MRP { get; set; }
        public DateTime Date { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}