using Microsoft.AspNetCore.Identity;

namespace Medictionary.Models
{
    public class User : IdentityUser
    {
        public required string UserId { get; set; } = Guid.NewGuid().ToString();
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string ContactNo { get; set; }
        public Image? ProfileImage { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; } = DateTime.UtcNow;
        public string? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}