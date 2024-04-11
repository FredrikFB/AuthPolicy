using AuthenticationAppUser.Context;
using AuthenticationAppUser.models;
using AuthenticationAppUser.Models;
using AuthenticationAppUser.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationAppUser.Repository
{
    public class SchoolRepository
    {
        private readonly ApplicationContext _context;
        private readonly RoleManager<IdentityRole> _role;
        private readonly UserManager<AppUser> _user;
        private readonly IMapper _mapper;

        public SchoolRepository(RoleManager<IdentityRole> role, UserManager<AppUser> user,ApplicationContext context, IMapper mapper)
        {
            _role = role;
            _user = user;
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> IsUniqueSchoolAsync(string schoolName)
        {
            var school = await _context.Schools.AnyAsync(s=> s.SchoolName == schoolName);
            if (!school)
            {
                return true;
            }

            return false;
        }

        public async Task<SchoolEntity> RegisterSchoolAsync(RegistrationSchoolDTO registerSchoolDTO, string userId)
        {
            try
            {
                SchoolEntity school = new()
                {
                    SchoolName = registerSchoolDTO.SchoolName,
                    OrgNr = registerSchoolDTO.OrgNr,
                };
                AddressEntity address = _mapper.Map<AddressEntity>(registerSchoolDTO);

                var result = _context.Schools.Add(school);

                if (result.Entity != null)
                {
                    var user = await _user.FindByIdAsync(userId);
                    if (user != null)
                    {
                        // Kontrollera om rollerna redan finns innan du skapar dem
                        if (!await _role.Roles.AnyAsync(x => x.Name == $"admin{school.SchoolId}"))
                        {
                            await _role.CreateAsync(new IdentityRole($"admin{school.SchoolId}"));
                            await _role.CreateAsync(new IdentityRole($"teacher{school.SchoolId}"));
                            await _role.CreateAsync(new IdentityRole($"student{school.SchoolId}"));
                        }
                        await _user.AddToRoleAsync(user, $"admin{school.SchoolId}");
                    }

                    SchoolAddressEntity schoolAddress;
                    var adressExists = await _context.Addresses.FirstOrDefaultAsync(x => x.StreetName == address.StreetName && x.Postalcode == address.Postalcode && x.City == address.City);
                    if (adressExists != null)
                    {
                        schoolAddress = new SchoolAddressEntity { AddressId = adressExists.AddressId, School = school };
                        _context.Add(schoolAddress);
                    }
                    else
                    {
                        schoolAddress = new SchoolAddressEntity { Address = address, School = school };
                        _context.Add(schoolAddress);
                    }

                    await _context.SaveChangesAsync(); // Spara alla ändringar i databasen
                    return school;
                }
                else
                {
                    return null!;
                }
            }
            catch (Exception ex)
            {
                return null!;
            }
        }
    }
}
