namespace Medictionary.DTOS
{
    public class StockiestRequestDTO
    {
        public int RequestID { get; set; }
        public string StockiestID { get; set; }
        public string IndustryID { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; }
        public string StockiestName { get; set; }
        public string IndustryName { get; set; }
    }
}