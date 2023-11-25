using System.Xml.Serialization;

namespace FileParser.Common.Status;

public abstract class BaseCombinedStatus : ICombinedStatus
{
    public ModuleState ModuleState { get; set; }

    public bool IsBusy { get; set; }
    public bool IsReady { get; set; }
    public bool IsError { get; set; }
    public bool KeyLock { get; set; }

    [XmlIgnore]
    public string TypeName => GetType().Name;
}

