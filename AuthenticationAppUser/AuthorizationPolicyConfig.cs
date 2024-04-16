using AuthenticationAppUser.Context;
using AuthenticationAppUser.Models.AuthRequirement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AuthenticationAppUser
{
    public static class AuthorizationPolicyConfig
    {
        public static void ConfigurePolicies(AuthorizationOptions options)
        {

            options.AddPolicy("RequireAdminRole", policy =>
            {
                policy.RequireAuthenticatedUser(); // Kräver att användaren är autentiserad
                policy.Requirements.Add(new RequireRole("admin")); // Kräver rollen "admin"
      
            });

            options.AddPolicy("RequireTeacherRole", policy =>
            {
                policy.Requirements.Add(new RequireRole("teacher"));
            });

            options.AddPolicy("RequireStudentRole", policy =>
            {
                policy.Requirements.Add(new RequireRole("student"));
            });


        }
    }
}
