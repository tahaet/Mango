using Mango.Services.EmailAPI.Models.Dto;

namespace Mango.Sevices.EmailAPI.Service
{
    interface IEmailService
    {
        Task EmailCartAndLog(CartDto cartDto);
        Task RegisterUserEmailAndLog(string email);
    }
}
