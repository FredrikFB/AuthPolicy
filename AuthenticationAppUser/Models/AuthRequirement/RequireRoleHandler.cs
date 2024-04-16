using AuthenticationAppUser.Context;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AuthenticationAppUser.Models.AuthRequirement
{
    public class RequireRoleHandler : AuthorizationHandler<RequireRole>
    {
        private readonly ApplicationContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RequireRoleHandler(ApplicationContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RequireRole requirement)
        {
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var schoolId = GetSchoolIdFromRoute(_httpContextAccessor);

            if (schoolId == 0)
            {
                context.Fail(); // Om SchoolId inte kan hittas, neka åtkomst
                return Task.CompletedTask;
            }

            // Kontrollera om användaren har den angivna rollen för den angivna skolan
            var hasRoleForSchool = _context.UserSchools
                .Any(u => u.UserId == userId && u.SchoolId == schoolId && u.Role == requirement.Role);

            if (hasRoleForSchool)
            {
                // Användaren har rollen för den angivna skolan, bevilja åtkomst
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }

        private int GetSchoolIdFromRoute(IHttpContextAccessor httpContextAccessor)
        {
            var data = httpContextAccessor.HttpContext?.Request.Query["id"];
            if ( int.TryParse(data, out int parsedSchoolId))
            {
                return parsedSchoolId;
            }
            return 0; // Om SchoolId inte kan hittas eller inte kan konverteras, returnera en standardvärde
        }
    }
}
