using DataProcessor.Application.Common.Interfaces;
using DataProcessor.Application.Common.VM;
using DataProcessor.Interfaces;
using Serilog;
using System.Text.Json;

namespace DataProcessor.Services;

public class DataProcessorService : IDataProcessor
{
    private readonly ILogger _logger;
    private readonly IModuleRepository _moduleRepository;

    public DataProcessorService(IModuleRepository moduleRepository, ILogger logger)
    {
        _moduleRepository = moduleRepository;
        _logger = logger;
    }

    public async Task ExecuteRawMessageAsync(string message, CancellationToken cancellationToken)
    {
        _logger.Debug("Begin read message of {length} chars", message.Length);
        try
        {
            _logger.Debug("Raw message: {message}", message);
            foreach (DeviceStatus device in JsonSerializer.Deserialize<InstrumentStatus>(message)!.DeviceStatus)
            {
                _logger.Debug("Device: {device}", device);
                await _moduleRepository.AddOrUpdateAsync(new Domain.Entities.Module()
                {
                    ModuleCategoryID = device.ModuleCategoryID,
                    ModuleState = device.RapidControlStatus.ModuleState
                }, cancellationToken);
            }
        }
        catch (Exception e)
        {
            _logger.Error(e, "Error read message '{message}'", message);
            return;
        }
        _logger.Debug("End read message of {length} chars", message.Length);
    }
}
