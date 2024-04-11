using AuthenticationAppUser.models;
using System.ComponentModel.DataAnnotations;

namespace AuthenticationAppUser.Models
{
    public class SchoolEntity
    {
        [Key]
        public int SchoolId { get; set; }

        public string SchoolName { get; set;}
        public string OrgNr { get; set; }

        public ICollection<UserAddressEntity> Addresses { get; set; } = new HashSet<UserAddressEntity>();

    }
}
