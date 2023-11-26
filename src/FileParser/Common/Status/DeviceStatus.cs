using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace FileParser.Common.Status;

[XmlRoot(ElementName = "DeviceStatus")]
public class DeviceStatus
{
    public required string ModuleCategoryID { get; set; }

    public required int IndexWithinRole { get; set; }

    [JsonIgnore]
    public required XmlStringCombinedStatus RapidControlStatus { get; set; }

    [JsonPropertyName("RapidControlStatus")]
    [XmlIgnore]
    public ICombinedStatus RapidControlStatusValue
    {
        get => RapidControlStatus.Value;
        set => RapidControlStatus.Value = value;
    }
}
