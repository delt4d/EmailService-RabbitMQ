using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using EmailService.ConsumerConsole.Models;

Console.WriteLine("Welcome to email service console consumer");

var factory = new ConnectionFactory()
{
    HostName = "rabbitmq",
    UserName = "root",
    Password = "root",
    VirtualHost = "/"
};
var conn = factory.CreateConnection();
using var channel = conn.CreateModel();
channel.QueueDeclare("emails", durable: true, exclusive: false);

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (ModuleHandle, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    var email = JsonConvert.DeserializeObject<Email>(message);

    if (email == null)
    {
        Console.WriteLine("Unable to deserialize incoming email: {message}");
        return;
    }

    Console.WriteLine(@$"
ID: {email.Id}
From: {email.From}
To: {email.Recipient}
Subject: {email.Subject}
Content: {email.Body}
    ");
};

channel.BasicConsume("emails", true, consumer);

while (true)
{
    // Add a small delay to avoid excessive CPU usage
    Thread.Sleep(1000); // 1 second delay
}

namespace EmailService.ConsumerConsole.Models
{
    public class Email
    {
        public string Id { get; set; } = "";
        public string From { get; set; } = "";
        public string Recipient { get; set; } = "";
        public string Body { get; set; } = "";
        public string Subject { get; set; } = "";
    }
}