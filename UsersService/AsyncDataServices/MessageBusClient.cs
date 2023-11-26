using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using UserService.Dtos;

namespace UserService.AsyncDataServices;

//клієнт шини повідомлень RabbitMq
public class MessageBusClient : IMessageBusClient
{
    private readonly IModel? _channel;
    private readonly IConnection? _connection;

    public MessageBusClient(IConfiguration configuration)
    {
        var factory = new ConnectionFactory
        {
            HostName = configuration["RabbitMQHost"],
            Port = int.Parse(configuration["RabbitMQPort"]!)
        };

        //підключення до шини RabbitMQ
        try
        {
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare("trigger", ExchangeType.Fanout);

            _connection.ConnectionShutdown += OnConnectionShutdown;
        }
        catch (Exception e)
        {
            Console.WriteLine($"--> Не вдалось підключитись до RabbitMq: {e.Message}");
        }
    }

    public void PublishUser(UserPublishDto userPublishDto)
    {
        var msg = JsonSerializer.Serialize(userPublishDto);

        if (_connection != null && _connection.IsOpen)
            SendMessage(msg);
        else
            Console.WriteLine("--> Не вдалось відправити повідомлення. З'єднання RabbitMQ прерване.");
    }

    //publish msg to MessageBus
    private void SendMessage(string msg)
    {
        var body = Encoding.UTF8.GetBytes(msg);
        _channel.BasicPublish("trigger", "", null, body);
        Console.WriteLine($"--> Відправлено: {msg}");
    }

    public void Dispose()
    {
        Console.WriteLine("--> RabbitMq закінчив роботу");

        if (_channel == null || !_channel.IsOpen) return;

        _channel.Close();
        _connection?.Close();
    }

    private static void OnConnectionShutdown(object? sender, ShutdownEventArgs e)
    {
        Console.WriteLine("--> З'єднання RabbitMQ закрито");
    }
}