using AuthenticationAppUser.models;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationAppUser.Models
{
    public class AppUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }


        public ICollection<SchoolEntity> Schools { get; set; } = new HashSet<SchoolEntity>();
        public ICollection<UserAddressEntity> Addresses { get; set; } = new HashSet<UserAddressEntity>();

        
    }
}
