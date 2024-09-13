namespace Mango.Utility.Email
{
    public interface IEmailSender
    {
        Task SendAsync(string to,string message, string subject = "");
    }
}
