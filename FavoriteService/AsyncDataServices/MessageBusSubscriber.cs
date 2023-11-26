using System.Text;
using FavoriteService.EventProcessing;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace FavoriteService.AsyncDataServices;

//клієнт шини повідомлень RabbitMq
public class MessageBusSubscriber : BackgroundService
{
    private readonly IModel? _channel;
    private readonly IConnection? _connection;
    private readonly IEventProcessor _eventProcessor;
    private readonly string? _queueName;

    public MessageBusSubscriber(IConfiguration configuration, IEventProcessor eventProcessor)
    {
        _eventProcessor = eventProcessor;

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
            _queueName = _channel.QueueDeclare().QueueName;

            _channel.ExchangeDeclare("trigger", ExchangeType.Fanout);
            _channel.QueueBind(_queueName, "trigger", "");

            _connection.ConnectionShutdown += OnConnectionShutdown;
        }
        catch (Exception e)
        {
            Console.WriteLine($"--> Не вдалось підключитись до RabbitMq: {e.Message}");
        }
    }

    //обробка повідомленнь з шини
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (_, ea) =>
        {
            Console.WriteLine("--> Повідомлення Отримно");
            var body = ea.Body; //get msg from bus
            var msg = Encoding.UTF8.GetString(body.ToArray()); //convert to string

            _eventProcessor.ProcessEvent(msg); //process it
        };

        _channel.BasicConsume(_queueName, true, consumer);
        return Task.CompletedTask;
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