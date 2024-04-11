using AuthenticationAppUser.Models;
using AuthenticationAppUser.Models.DTO;
using AuthenticationAppUser.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AuthenticationAppUser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserRepository _userRepo;
        protected ApiResponse _Response;
        public readonly SignInManager<AppUser> _signInManager;
        public readonly UserManager<AppUser> _userManager;

        public AuthController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, UserRepository userRepo)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userRepo = userRepo;
            _Response = new();
        }


        [HttpPost("registerschool")]
        public async Task<ActionResult<ApiResponse>> RegisterUser([FromBody] RegisterUserDTO RegisterDTO)
        {
            try
            {
                bool ifEmailUnique = await _userRepo.IsUniqueUser(RegisterDTO.Email);
                if (!ifEmailUnique)
                {
                    _Response.StatusCode = HttpStatusCode.BadRequest;
                    _Response.IsSuccess = false;
                    _Response.ErrorMessages.Add("Username already exists");
                    return BadRequest(_Response);
                }

                var user = await _userRepo.RegisterUserAsync(RegisterDTO);
                if (user == null)
                {
                    _Response.StatusCode = HttpStatusCode.BadRequest;
                    _Response.IsSuccess = false;
                    _Response.ErrorMessages.Add("Error while registering");
                    return BadRequest(_Response);
                }
                _Response.StatusCode = HttpStatusCode.OK;
                return Ok(_Response);
            }
            catch (Exception ex)
            {
                _Response.IsSuccess = false;
                _Response.ErrorMessages = [ex.ToString()];
            }
            return _Response;

        }


        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return Ok("Logout Successfull!, We hope you come back :)");
            }
            catch
            {
                return NoContent();
            }

        }
    }
}

