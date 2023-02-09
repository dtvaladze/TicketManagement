using Microsoft.AspNetCore.Identity;

namespace TicketManagement.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Password { get; set; } 
        public RoleEnum Role { get; set; }
    }
}
