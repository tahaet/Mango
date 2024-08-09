using Mango.Services.AuthAPI.Data;
using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly AppDbContext db;
        private readonly IJwtTokenGenerator jwtTokenGenerator;

        public AuthService(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager,AppDbContext db,IJwtTokenGenerator jwtTokenGenerator)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.db = db;
            this.jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = db.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
            if (user != null)
            {
                if (!roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    //create role if it does not exist
                    roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
           var user =  db.ApplicationUsers.FirstOrDefault(x=>x.UserName.ToLower()==loginRequestDto.UserName.ToLower());  
            bool isValid = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);    
            if(user is null || !isValid)
            {
                return new LoginResponseDto()
                {
                    User=null,
                    Token=""
                };
            }
            var roles = await userManager.GetRolesAsync(user);
            var token = jwtTokenGenerator.GenerateToken(user,roles);
            UserDto userDto = new UserDto()
            {
                ID = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email
            };
            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = userDto,
                Token = token
            };
            return loginResponseDto;
        }

        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = registrationRequestDto.Email,
                Email = registrationRequestDto.Email,
                NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                Name = registrationRequestDto.Name,
                PhoneNumber = registrationRequestDto.PhoneNumber,
            };
            try
            {
                var result = await userManager.CreateAsync(user,registrationRequestDto.Password);
                if(result.Succeeded) 
                {
                    var userToReturn = db.ApplicationUsers.FirstOrDefault(x=>x.Email == registrationRequestDto.Email);
                    UserDto userDto = new UserDto()
                    {
                        ID = userToReturn.Id,
                        Name= userToReturn.Name,
                        Email= userToReturn.Email,
                        PhoneNumber = userToReturn.PhoneNumber
                    };
                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }

            }
            catch (Exception ex)
            {

            }
            return "Error encountered";
        }
    }
}
