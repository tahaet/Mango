using Mango.Services.EmailAPI.Models.Dto;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Mango.Services.EmailAPI.Data;

using Mango.Sevices.EmailAPI.Models;
using Mango.Services.EmailAPI.Message;
namespace Mango.Sevices.EmailAPI.Service
{
    public class EmailService : IEmailService
    {
        private DbContextOptions<AppDbContext> _dbOptions;
        private readonly IServiceProvider serviceProvider;

        public EmailService(DbContextOptions<AppDbContext> dbOptions, IServiceProvider serviceProvider)
        {
            _dbOptions = dbOptions;
            this.serviceProvider = serviceProvider;
        }

        public async Task EmailCartAndLog(CartDto cartDto)
        {
            StringBuilder message = new StringBuilder();

            message.AppendLine("<br/>Cart Email Requested ");
            message.AppendLine("<br/>Total " + cartDto.CartHeader.CartTotal);
            message.Append("<br/>");
            message.Append("<ul>");
            foreach (var item in cartDto.CartDetails)
            {
                message.Append("<li>");
                message.Append(item.Product.Name + " x " + item.Count);
                message.Append("</li>");
            }
            message.Append("</ul>");

            await LogAndEmail(message.ToString(), cartDto.CartHeader.Email);
            await SendEmail(cartDto.CartHeader.Email, message.ToString(), "New Order");
        }


        public async Task SendEmail(string to, string body, string subject)
        {
            // Create a scope to resolve the scoped service
            using (var scope = serviceProvider.CreateScope())
            {
                var emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();

                // Now you can use the scoped EmailSender service within this method
                await emailSender.SendAsync(to, body,subject);
            }
        }

        public async Task LogOrderPlaced(RewardsMessage rewardsMessage)
        {
            string message = "New order placed. <br/> Order ID : " + rewardsMessage.OrderId;
            await LogAndEmail(message, "user@gmail.com");
        }

        public async Task RegisterUserEmailAndLog(string email)
        {
            string message = "User Registeration Successful. <br/> Email : " + email;
            await LogAndEmail(message, email);
        }

        private async Task<bool> LogAndEmail(string message, string email)
        {
            try
            {
                EmailLogger emailLog = new()
                {
                    Email = email,
                    EmailSent = DateTime.Now,
                    Message = message
                };
                await using var _db = new AppDbContext(_dbOptions);
                await _db.EmailLoggers.AddAsync(emailLog);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

}
