using Microsoft.AspNetCore.Identity.UI.Services;

namespace Asp.NetCoreMastery.Models
{
    public class HtmlEmailSender : IEmailSender
    {
        public void SendEmail(string receiverEmail, string subject, string body)
        {
            throw new NotImplementedException();
        }
    }
}
