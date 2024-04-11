using Microsoft.AspNetCore.Identity;

namespace Authentication.Models
{
    public class AppUser : IdentityUser
    {
        public string? SchoolId {  get; set; }
        public string Address { get; set; }
    }
}
