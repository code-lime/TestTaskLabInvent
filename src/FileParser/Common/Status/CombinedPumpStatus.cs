namespace FileParser.Common.Status;

public class CombinedPumpStatus : BaseCombinedStatus
{
    public string? Mode { get; set; }
    public int Flow { get; set; }
    public int PercentB { get; set; }
    public int PercentC { get; set; }
    public int PercentD { get; set; }
    public int MinimumPressureLimit { get; set; }
    public double MaximumPressureLimit { get; set; }
    public int Pressure { get; set; }
    public bool PumpOn { get; set; }
    public int Channel { get; set; }
}

