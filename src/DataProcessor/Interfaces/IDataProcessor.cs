namespace DataProcessor.Interfaces;

public interface IDataProcessor
{
    Task ExecuteRawMessageAsync(string message, CancellationToken cancellationToken);
}
