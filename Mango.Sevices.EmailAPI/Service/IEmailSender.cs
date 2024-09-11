namespace Mango.Sevices.EmailAPI.Service
{
    public interface IEmailSender
    {
        Task SendAsync(string to,string message, string subject = "");
    }
}
