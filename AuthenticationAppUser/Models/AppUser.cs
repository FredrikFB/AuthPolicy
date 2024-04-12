using AuthenticationAppUser.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationAppUser.Models
{
    public class AppUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }


        public ICollection<UserSchoolEntity> Schools { get; set; } = new HashSet<UserSchoolEntity>();
        public ICollection<UserAddressEntity> Addresses { get; set; } = new HashSet<UserAddressEntity>();

       
    }
}
