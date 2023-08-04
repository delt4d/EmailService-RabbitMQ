using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace EmailService.API.Services;

public class AppConnection : IAppConnection
{
    private readonly ConnectionFactory _factory = new()
    {
        HostName = "rabbitmq",
        UserName = "root",
        Password = "root",
        VirtualHost = "/"
    };

    public void BasicPublish<T>(IModel channel, T message)
    {
        var jsonString = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(jsonString);

        channel.BasicPublish("", "emails", body: body);
    }

    public IModel CreateChannel(IConnection conn)
    {
        var channel = conn.CreateModel();
        channel.QueueDeclare("emails", durable: true, exclusive: false);
        return channel;
    }

    public IConnection CreateConnection()
    {
        return _factory.CreateConnection();
    }
}