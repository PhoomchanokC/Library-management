using Microsoft.AspNetCore.Identity;

namespace Library_management.Models
{
    public class ApplicaitonUser :IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        
        public DateTime CreateDate { get; set; }
    }
}
