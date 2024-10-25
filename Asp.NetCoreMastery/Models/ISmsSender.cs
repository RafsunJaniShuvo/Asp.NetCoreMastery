namespace Asp.NetCoreMastery.Models
{
    public interface ISmsSender
    {
        void SendSms(string mobile,string message);
    }
}
