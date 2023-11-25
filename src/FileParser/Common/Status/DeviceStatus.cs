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

    /*
    [JsonIgnore]
    [XmlElement(ElementName = "RapidControlStatus")]
    public string RapidControlStatus_Raw
    {
        get
        {
            using StringWriter writer = new StringWriter();
            XmlSerializer converter = new XmlSerializer(RapidControlStatus.GetType());
            converter.Serialize(writer, this);
            return writer.ToString();
        }
        set
        {
            using StringReader reader = new StringReader(value);
            XmlSerializer converter = new XmlSerializer(typeof(ICombinedStatus), new Type[] {
                typeof(CombinedOvenStatus),
                typeof(CombinedPumpStatus),
                typeof(CombinedSamplerStatus)
            });
            RapidControlStatus = (ICombinedStatus)converter.Deserialize(reader)!;
        }
    }*/
}
