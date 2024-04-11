using Microsoft.AspNetCore.Identity;

namespace Authentication.Models
{
    public class StudentUser : AppUser
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }

        public string? SchoolId { get; set; }
    }
}