namespace EmailService.API.Services;

public interface IAppConnection
{
    RabbitMQ.Client.IConnection CreateConnection();
    RabbitMQ.Client.IModel CreateChannel(RabbitMQ.Client.IConnection conn);
    void BasicPublish<T>(RabbitMQ.Client.IModel channel, T message);
}