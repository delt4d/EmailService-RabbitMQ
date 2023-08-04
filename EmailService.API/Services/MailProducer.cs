using EmailService.API.Models;

namespace EmailService.API.Services;

class MailProducer : IMailProducer
{
    private readonly IAppConnection _appConnection;

    public MailProducer(IAppConnection appConnection)
    {
        _appConnection = appConnection;
    }

    public void SendEmailMessage(Email email)
    {
        var conn = _appConnection.CreateConnection();
        using var channel = _appConnection.CreateChannel(conn);

        _appConnection.BasicPublish(channel, email);
    }
}