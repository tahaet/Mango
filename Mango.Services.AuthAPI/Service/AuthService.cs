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

        public AuthService(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager,AppDbContext db)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.db = db;
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
                Token = ""
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
