namespace FileParser.Common.Configs;

public class FileListenerConfig
{
    public const string SectionKey = "FileListener";

    public required string Directory { get; set; }
    public required double TimerDelaySec { get; set; }
}
