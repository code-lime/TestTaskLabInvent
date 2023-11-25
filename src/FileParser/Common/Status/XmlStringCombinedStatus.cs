using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml;

namespace FileParser.Common.Status;

public sealed class XmlStringCombinedStatus : IXmlSerializable
{
    private static readonly Dictionary<string, Type> TYPES = new Dictionary<string, Type>()
    {
        [nameof(CombinedOvenStatus)] = typeof(CombinedOvenStatus),
        [nameof(CombinedPumpStatus)] = typeof(CombinedPumpStatus),
        [nameof(CombinedSamplerStatus)] = typeof(CombinedSamplerStatus),
    };

    public ICombinedStatus Value { get; set; }

    public XmlStringCombinedStatus()
        => Value = default!;
    public XmlStringCombinedStatus(ICombinedStatus value)
        => Value = value;

    public void WriteXml(XmlWriter writer) => throw new NotSupportedException();
    /*
    XmlSerializer converter = new XmlSerializer(Value.GetType());
    using StringWriter stringWriter = new StringWriter();
    converter.Serialize(stringWriter, this);
    writer.WriteString(stringWriter.ToString());*/

    public void ReadXml(XmlReader reader)
    {
        string source = reader.ReadElementContentAsString();
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(source);
        using StringReader stringReader = new StringReader(source);
        XmlSerializer converter = new XmlSerializer(TYPES[doc.DocumentElement!.Name]);
        using XmlReader xmlReader = new XmlNodeReader(doc);
        Value = (ICombinedStatus)converter.Deserialize(xmlReader)!;
    }

    public XmlSchema GetSchema() => null!;
}