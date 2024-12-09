namespace FMSEvaluering.Application.MailService;

public interface IEmailSender
{
    void SendEmail(string toAddress, string message);
}