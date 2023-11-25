using System.Text.Json;
using System.Text;
using FileParser.Interfaces;
using RabbitMQ.Client;
using Microsoft.Extensions.Options;
using System.Text.Json.Nodes;
using FileParser.Common.Configs;
using Serilog;

namespace FileParser.Services;

public class RabbitMqSenderService : IQuerySender
{
    private readonly ILogger _logger;
    private readonly RabbitMqConfig _config;

    public RabbitMqSenderService(IOptions<RabbitMqConfig> config, ILogger logger)
    {
        _config = config.Value;
        _logger = logger;
    }

    public void SendRawMessage(string message)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(message);
        
        _logger.Debug("Begin send message of {bytes}B", bytes.Length);
        ConnectionFactory factory = new ConnectionFactory
        {
            Uri = new Uri(_config.Url),
            DispatchConsumersAsync = true,
        };
        try
        {
            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: _config.Queue,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            channel.BasicPublish(
                exchange: string.Empty,
                routingKey: _config.Queue,
                basicProperties: null,
                body: bytes);
        }
        catch (Exception e)
        {
            _logger.Error(e, "Error send message '{message}'", message);
            return;
        }
        _logger.Debug("End send message of {bytes}B", bytes.Length);
    }
    public void SendJsonNode(JsonNode node)
        => SendRawMessage(node.ToJsonString());
    public void SendRawObject(object raw)
        => SendRawMessage(JsonSerializer.Serialize(raw));
}
