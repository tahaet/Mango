using Azure;
using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Service;
using Mango.Services.AuthAPI.Service.IService;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Mango.Services.AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService authService;
        private ResponseDto responseDto;
        public AuthAPIController(IAuthService authService)
        {
            this.authService = authService;
            responseDto = new ResponseDto();
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegistrationRequestDto registrationRequestDto)
        {
            var errorMessage = await authService.Register(registrationRequestDto);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                responseDto.IsSuccess = false;
                responseDto.Message = errorMessage;
                return BadRequest(responseDto);
            }
            return Ok(responseDto);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var loginResponse = await authService.Login(loginRequestDto);
            if (loginResponse.User is null)
            {
                responseDto.IsSuccess = false;
                responseDto.Message = "Username or password is incorrect";
                return BadRequest(responseDto);
            }
            responseDto.Result= loginResponse;
            return Ok(responseDto);
        }
        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto model)
        {
            var assignRoleSuccessful = await authService.AssignRole(model.Email, model.Role.ToUpper());
            if (!assignRoleSuccessful)
            {
                responseDto.IsSuccess = false;
                responseDto.Message = "Error encountered";
                return BadRequest(responseDto);
            }
            return Ok(responseDto);

        }
    }
}
