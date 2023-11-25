namespace DataProcessor.Configs;

public class RabbitMqConfig
{
    public const string SectionKey = "RabbitMq";

    public required string Url { get; set; }
    public required string Queue { get; set; }
}
