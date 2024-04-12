using AuthenticationAppUser.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationAppUser.Models
{
    [PrimaryKey(nameof(SchoolId), nameof(UserId))]
    public class UserSchoolEntity
    {
        public int SchoolId { get; set; }
        public SchoolEntity School { get; set; } = null!;

        public string UserId { get; set; }
        public AppUser User { get; set; } = null!;

    }
}
