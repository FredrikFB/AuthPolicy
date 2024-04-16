using Microsoft.AspNetCore.Authorization;

namespace AuthenticationAppUser.Models.AuthRequirement
{
    public class RequireRole : IAuthorizationRequirement
    {
        public string Role { get; }

        public RequireRole(string role)
        {
            Role = role;
        }
    }
}
