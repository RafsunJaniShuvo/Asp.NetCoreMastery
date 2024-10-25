namespace Asp.NetCoreMastery.Models
{
    public interface IEmailSender
    {
        void SendEmail(string receiverEmail, string subject, string body);
    }
}
