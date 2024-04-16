using AuthenticationAppUser.Context;
using AuthenticationAppUser.Models;
using AuthenticationAppUser.Models.DTO;
using AuthenticationAppUser.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Net;
using System.Security.Claims;

namespace AuthenticationAppUser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly SchoolRepository _schoolRepo;
        protected ApiResponse _response;
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationContext _context;

        public SchoolController(SchoolRepository schoolRepo, ApplicationContext context, UserManager<AppUser> userManager)
        {
            _schoolRepo = schoolRepo;
            _context = context;
            _response = new();
            _userManager = userManager;
        }

        [HttpPost("RegisterSchool")]
        [Authorize]
        public async Task<ActionResult<ApiResponse>> RegisterSchool([FromBody] RegistrationSchoolDTO registerSchoolDTO)
        {
            try
            {
                bool ifSchoolUnique = await _schoolRepo.IsUniqueSchoolAsync(registerSchoolDTO.SchoolName);
                if (!ifSchoolUnique)
                    return BadRequest(new ApiResponse(HttpStatusCode.BadRequest, "Username already exists."));

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var school = await _schoolRepo.RegisterSchoolAsync(registerSchoolDTO, userId! );
                if (school == null)
                    return BadRequest(new ApiResponse(HttpStatusCode.BadRequest, "Error while registering school."));

                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = [ex.ToString()];
            }
            return _response;
        }

        [HttpPost("AddUserToRole")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<ActionResult<string>> AddRoleToUser(int id)
        {
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var user = await _userManager.GetUserAsync(User);
            //var schools= user.Schools.ToList();

            return "works";
        }

    }
}

