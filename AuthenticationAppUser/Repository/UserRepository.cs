using AuthenticationAppUser.Context;
using AuthenticationAppUser.Models;
using AuthenticationAppUser.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationAppUser.Repository
{
    public class UserRepository
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public UserRepository(ApplicationContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
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

       

        public async Task<AppUser> RegisterUserAsync(RegisterUserDTO registrationDTO)
        {

            try
            {
                AppUser user = new()
                {
                    UserName = registrationDTO.Email,
                    Email = registrationDTO.Email,
                    NormalizedEmail = registrationDTO.Email.ToUpper(),
                    PhoneNumber = registrationDTO.PhoneNumber,
                    FirstName = registrationDTO.FirstName,
                    LastName = registrationDTO.LastName,
                };

                AddressEntity address = _mapper.Map<AddressEntity>(registrationDTO);

                var result = await _userManager.CreateAsync(user, registrationDTO.Password);
                if (result.Succeeded)
                {
                    //lägg till för roles hantering här
                    if (!await _roleManager.Roles.AnyAsync())
                    {
                        await _roleManager.CreateAsync(new IdentityRole("user"));
                    }

                    if (await _roleManager.Roles.AnyAsync(x => x.Name == registrationDTO.Role))
                    {
                        await _userManager.AddToRoleAsync(user, registrationDTO.Role);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "user");
                    }

                    UserAddressEntity userAddress;
                    var adressExists = await _context.Addresses.FirstOrDefaultAsync(x => x.StreetName == address.StreetName && x.Postalcode == address.Postalcode && x.City == address.City);
                    if (adressExists != null)
                    {
                        userAddress = new UserAddressEntity { AddressId = adressExists.AddressId, UserId = user.Id };
                        _context.Add(userAddress);
                    }
                    else
                    {
                        userAddress = new UserAddressEntity { Address = address, UserId = user.Id };
                        _context.Add(userAddress);
                    }


                    await _context.SaveChangesAsync();

                    var userToReturn = await _userManager.FindByEmailAsync(registrationDTO.Email);

                    return userToReturn!;
                }

                return null!;
            }
            catch (Exception ex)
            {
               
            }
            return new AppUser();
        }
    }
}

