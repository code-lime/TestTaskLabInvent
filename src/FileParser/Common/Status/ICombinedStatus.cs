namespace FileParser.Common.Status;

public interface ICombinedStatus
{
    ModuleState ModuleState { get; set; }

    bool IsBusy { get; set; }
    bool IsError { get; set; }
    bool IsReady { get; set; }
    bool KeyLock { get; set; }

    string TypeName { get; }
}