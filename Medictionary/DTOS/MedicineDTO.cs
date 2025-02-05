namespace Medictionary.DTOS
{
    public class MedicineDTO
    {
        public string IndustryID { get; set; }
        public string Name { get; set; }
        public string Composition { get; set; }
        public string Manufacturer { get; set; }
        public string Batch { get; set; }
        public string ManufacturingDate { get; set; }
        public string ExpiryDate { get; set; }
        public decimal Price { get; set; }
        public string Stock { get; set; }
    }
}