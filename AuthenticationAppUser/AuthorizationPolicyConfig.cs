using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AuthenticationAppUser
{
    public static class AuthorizationPolicyConfig
    {
        public static void ConfigurePolicies(AuthorizationOptions options)
        {
            options.AddPolicy("RequireAdminRole", policy =>
            {
                policy.RequireAssertion(context =>
                {
                    var roleClaims = context.User.Claims.Where(c => c.Type == ClaimTypes.Role).ToList();
                    var roles = roleClaims.Select(c => c.Value).ToList();

                    //var schoolId = context.User.FindFirstValue("");

                    //return context.User.IsInRole($"admin{schoolId}");
                    return context.User.IsInRole("admin1");
                });
            });

            options.AddPolicy("RequireTeacherRole", policy =>
            {
                policy.RequireAssertion(context =>
                {
                    var schoolId = context.User.FindFirstValue("SchoolId");

                    return context.User.IsInRole("teacher" + schoolId);
                });
            });
        }
    }
}
