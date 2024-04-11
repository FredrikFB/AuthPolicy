using AuthenticationAppUser.models;
using AuthenticationAppUser.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationAppUser.Context
{
    public class ApplicationContext : IdentityDbContext<AppUser>
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AddressEntity> Addresses { get; set; }
        public DbSet<UserAddressEntity> UserAddresses { get; set; }
        public DbSet<SchoolAddressEntity> SchoolAddresses { get; set; }
        public DbSet<SchoolEntity> Schools { get; set; }

        
    
    }
}
