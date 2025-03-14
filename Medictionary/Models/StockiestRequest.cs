using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medictionary.Models
{
    public class StockiestRequest
    {
        [Key]
        public int RequestID { get; set; }
        public string StockiestID { get; set; }
        public string IndustryID { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; }

        [ForeignKey("StockiestID")]
        public User Stockiest { get; set; }

        [ForeignKey("IndustryID")]
        public Industry Industry { get; set; }
    }
}