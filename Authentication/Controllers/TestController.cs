using AbliTest.models;
using Authentication.Models;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;

namespace Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roles;
        private readonly UserManager<AppUser> _userManager;
        private readonly ApiResponse _Response;


        public TestController(RoleManager<IdentityRole> roles, UserManager<AppUser> userManager)
        {
            _roles = roles;
            _userManager = userManager;
            _Response = new();
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<string>> Test()
        {
            var user = await _userManager.GetUserAsync(User);
            if ((user == null))
            {
                return Unauthorized();
            }

            var userRole = await _userManager.GetRolesAsync(user);

            if (!await _roles.RoleExistsAsync("admin" + user.SchoolId) && !userRole.Contains("admin" + user.SchoolId))
            {
                return Unauthorized();
            }


            return "Hello";
        }

        [HttpGet("GetPrograms")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetPrograms([FromQuery] string? search, int pageSize = 0, int pageNumber = 1)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if ((user == null))
                {
                    return Unauthorized();
                }

                var userRole = await _userManager.GetRolesAsync(user);

                if (!await _roles.RoleExistsAsync("admin" + user.SchoolId) && !userRole.Contains("admin" + user.SchoolId))
                {
                    return Unauthorized();
                }
                //då är man admin på den skolan




                return Ok(_Response);
            }
            catch (Exception ex)
            {
                _Response.IsSuccess = false;
                _Response.ErrorMessages = [ex.ToString()];
            }
            return _Response;
        }

    }
}
