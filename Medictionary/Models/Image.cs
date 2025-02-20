namespace Medictionary.Models
{
    public class Image : Document
    {
        public required string FileName { get; set; }
        public required string FilePath { get; set; }
    }
}