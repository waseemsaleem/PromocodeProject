using Microsoft.AspNetCore.Identity;

namespace PromoCodeProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Email { get; set; }
    }
}