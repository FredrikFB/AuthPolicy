using Authentication.Context;
using Authentication.Models;
using Authentication.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AbliTest.Repository
{
    public class UserRepository 
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly UserManager<School> _schoolManager;
        private readonly UserManager<StudentUser> _studentManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRepository(ApplicationContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, UserManager<School> schoolManager, UserManager<StudentUser> studentManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _schoolManager = schoolManager;
            _studentManager = studentManager;
        }
        
        
        public async Task<bool> IsUniqueUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return true;
            }
            
            return false;
        }

        public async Task<School> SchoolRegister(SchoolRegisterDTO registrationDTO)
        {

            School user = new()
            {
                UserName = registrationDTO.Email,
                Email = registrationDTO.Email,
                NormalizedEmail = registrationDTO.Email.ToUpper(),
                PhoneNumber = registrationDTO.PhoneNumber,
                Address = registrationDTO.Address,
                SchoolName = registrationDTO.SchoolName
            };


            try
            {
                var result = await _userManager.CreateAsync(user, registrationDTO.Password);
                if (result.Succeeded)
                {
                    //lägg till för roles hantering här
                    if (!await _roleManager.Roles.AnyAsync())
                    {
                        await _roleManager.CreateAsync(new IdentityRole("student"));
                        await _roleManager.CreateAsync(new IdentityRole("utbildningsanordnare"));
                        await _roleManager.CreateAsync(new IdentityRole("teacher"));
                        await _roleManager.CreateAsync(new IdentityRole("company"));
                        await _roleManager.CreateAsync(new IdentityRole("school"));
                        await _roleManager.CreateAsync(new IdentityRole("user"));
                        //await _roleManager.CreateAsync(new IdentityRole($"teacher{schoolId}"));
                    }

                    await _roleManager.CreateAsync(new IdentityRole($"admin{user.Id}"));


                    if (await _roleManager.Roles.AnyAsync(x=> x.Name == "school"))
                    {

                        await _userManager.AddToRoleAsync(user, $"school{user.Id}");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "user");
                    }


                    await _context.SaveChangesAsync();

                    var userToReturn = await _schoolManager.FindByEmailAsync(registrationDTO.Email);

                    return userToReturn!;
                }
                      
                return null!;
            }
            catch (Exception ex)
            {

            }
            return new School();
        }

        public async Task<StudentUser> StudentRegister(StudentRegisterDTO registrationDTO)
        {

            StudentUser user = new()
            {
                UserName = registrationDTO.Email,
                Email = registrationDTO.Email,
                NormalizedEmail = registrationDTO.Email.ToUpper(),
                PhoneNumber = registrationDTO.PhoneNumber,
                Address = registrationDTO.Address,
                FirstName = registrationDTO.FirstName,
                LastName = registrationDTO.LastName,
            };


            try
            {
                var result = await _userManager.CreateAsync(user, registrationDTO.Password);
                if (result.Succeeded)
                {
                    //lägg till för roles hantering här
                    if (!await _roleManager.Roles.AnyAsync())
                    {
                        await _roleManager.CreateAsync(new IdentityRole("student"));
                        await _roleManager.CreateAsync(new IdentityRole("utbildningsanordnare"));
                        await _roleManager.CreateAsync(new IdentityRole("teacher"));
                        await _roleManager.CreateAsync(new IdentityRole("company"));
                        await _roleManager.CreateAsync(new IdentityRole("school"));
                        await _roleManager.CreateAsync(new IdentityRole("user"));
                    }

                    if (await _roleManager.Roles.AnyAsync(x => x.Name == "student"))
                    {
                        await _userManager.AddToRoleAsync(user, "student");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "user");
                    }


                    await _context.SaveChangesAsync();

                    var userToReturn = await _studentManager.FindByEmailAsync(registrationDTO.Email);

                    return userToReturn!;
                }

                return null!;
            }
            catch (Exception ex)
            {

            }
            return new StudentUser();
        }
    }
}
