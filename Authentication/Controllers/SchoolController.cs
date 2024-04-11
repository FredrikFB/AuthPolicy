using Authentication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        public SchoolController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        
        public async Task<ActionResult> AddUserToRole(string userMail, string schoolId, string role)
        {
            var user = await _userManager.FindByEmailAsync(userMail);
            if (user == null)
            {
                return BadRequest();
            }
            user.SchoolId = schoolId;
            var roleResult = await _userManager.AddToRoleAsync(user, role);
            if (!roleResult.Succeeded)
            {
                // Återställ värdet på SchoolId om tilldelningen av rollen misslyckades
                user.SchoolId = null;
                return BadRequest();

            }
            var result = await _userManager.UpdateAsync(user);

            return Ok();
        }
    }
}
