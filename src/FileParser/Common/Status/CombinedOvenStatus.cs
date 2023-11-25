using System.Xml.Serialization;

namespace FileParser.Common.Status;

public class CombinedOvenStatus : BaseCombinedStatus
{
    public bool UseTemperatureControl { get; set; }

    public bool OvenOn { get; set; }

    [XmlElement(ElementName = "Temperature_Actual")]
    public double TemperatureActual { get; set; }

    [XmlElement(ElementName = "Temperature_Room")]
    public double TemperatureRoom { get; set; }

    public int MaximumTemperatureLimit { get; set; }

    [XmlElement(ElementName = "Valve_Position")]
    public int ValvePosition { get; set; }

    [XmlElement(ElementName = "Valve_Rotations")]
    public int ValveRotations { get; set; }

    public bool Buzzer { get; set; }
}

