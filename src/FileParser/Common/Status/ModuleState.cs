using System.Text.Json.Serialization;

namespace FileParser.Common.Status;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ModuleState
{
    Online,
    Run,
    NotReady,
    Offline
}

