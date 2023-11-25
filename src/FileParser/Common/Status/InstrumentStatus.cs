using System.Xml;
using System.Xml.Serialization;

namespace FileParser.Common.Status;

[XmlRoot(ElementName = "InstrumentStatus")]
public class InstrumentStatus
{
    public required string PackageID { get; set; }

    [XmlElement(ElementName = "DeviceStatus")]
    public required List<DeviceStatus> DeviceStatus { get; set; }
}