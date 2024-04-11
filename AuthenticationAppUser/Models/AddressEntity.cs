using System.ComponentModel.DataAnnotations;

namespace AuthenticationAppUser.models
{
    public class AddressEntity
    {
        [Key] 
        public int AddressId { get; set; }
        public string StreetName { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Postalcode { get; set; } = null!;

    }
}
