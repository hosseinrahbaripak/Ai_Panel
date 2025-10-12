namespace Ai_Panel.Infrastructure.Contracts.Notification;
public interface ISmsManagerService
{
    Task<bool> Login(string mobile, string code);
    Task<bool> PaymentSuccess(string mobile, string name, string amount);
    Task<bool> SendMessage(object payLoad, string destination);
}
