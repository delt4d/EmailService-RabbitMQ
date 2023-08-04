using EmailService.API.Models;

namespace EmailService.API.Services;

public interface IMailProducer
{
    public void SendEmailMessage(Email email);
}