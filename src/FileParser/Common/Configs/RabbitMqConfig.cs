namespace FileParser.Common.Configs;

public class RabbitMqConfig
{
    public const string SectionKey = "RabbitMq";

    public required string Url { get; set; }
    public required string Queue { get; set; }
}
