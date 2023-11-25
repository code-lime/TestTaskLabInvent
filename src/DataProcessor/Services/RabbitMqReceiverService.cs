using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using Microsoft.Extensions.Options;
using System.Text;
using DataProcessor.Interfaces;
using DataProcessor.Configs;
using Serilog;

namespace DataProcessor.Services;

public class RabbitMqReceiverService : BackgroundService
{
    private readonly ILogger _logger;

    private readonly IDataProcessor _processor;

    private readonly IConnection _connection;
    private readonly IModel _channel;

    private readonly RabbitMqConfig _config;

    public RabbitMqReceiverService(IDataProcessor processor, IOptions<RabbitMqConfig> config, ILogger logger)
    {
        _processor = processor;
        _config = config.Value;

        ConnectionFactory factory = new ConnectionFactory
        {
            Uri = new Uri(_config.Url),
            DispatchConsumersAsync = true,
        };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(
            queue: _config.Queue,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        _logger = logger;
    }

    protected override Task ExecuteAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        try
        {
            AsyncEventingBasicConsumer consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += async (s, e) =>
            {
                string message = Encoding.UTF8.GetString(e.Body.Span);
                await _processor.ExecuteRawMessageAsync(message, cancellationToken);
                _channel.BasicAck(e.DeliveryTag, false);
            };

            _channel.BasicConsume(_config.Queue, false, consumer);
        }
        catch (Exception e)
        {
            _logger.Error(e, "Error consume RabbitMQ");
        }
        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        _channel.Close();
        _connection.Close();
        base.Dispose();
    }
}
