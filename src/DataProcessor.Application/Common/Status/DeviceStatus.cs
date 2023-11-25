namespace DataProcessor.Application.Common.VM;

public class DeviceStatus
{
    public string ModuleCategoryID { get; set; } = null!;
    public CombinedStatus RapidControlStatus { get; set; } = null!;
}
