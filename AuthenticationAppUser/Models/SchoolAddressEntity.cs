using AuthenticationAppUser.models;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationAppUser.Models
{
    [PrimaryKey(nameof(SchoolId),nameof(AddressId))]
    public class SchoolAddressEntity
    {
        public int SchoolId { get; set; }
        public SchoolEntity School { get; set; } = null!;

        public int AddressId { get; set; }
        public AddressEntity Address { get; set; } = null!;
    }
}
