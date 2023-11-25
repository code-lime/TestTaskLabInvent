using FileParser.Common.Configs;
using FileParser.Common.Status;
using FileParser.Interfaces;
using Microsoft.Extensions.Options;
using Serilog;

namespace FileParser.Services;

public class FileListenerService : IHostedService, IDisposable
{
    private static readonly ModuleState[] MODULE_STATES = Enum.GetValues<ModuleState>();

    private readonly ILogger _logger;

    private readonly IFileParser _parser;
    private readonly IQuerySender _messenger;

    private readonly FileListenerConfig _config;

    private CancellationTokenSource? cancellationTokenSource;

    public FileListenerService(IFileParser parser, IQuerySender messenger, IOptions<FileListenerConfig> config, ILogger logger)
    {
        _parser = parser;
        _messenger = messenger;
        _config = config.Value;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        cancellationTokenSource?.Cancel();
        cancellationTokenSource = new CancellationTokenSource();
        _logger.Information("Start file listener in directory '{directory}'", Path.GetFullPath(_config.Directory));
        _ = DoLoop(cancellationTokenSource.Token);
        return Task.CompletedTask;
    }

    private async Task DoLoop(CancellationToken cancellationToken)
    {
        while (true)
        {
            await Task.Delay(TimeSpan.FromSeconds(_config.TimerDelaySec), cancellationToken);
            try
            {
                foreach (InstrumentStatus instrument in _parser.ParseFiles(Path.GetFullPath(_config.Directory)))
                {
                    foreach (DeviceStatus device in instrument.DeviceStatus)
                        device.RapidControlStatusValue.ModuleState = MODULE_STATES[Random.Shared.Next(MODULE_STATES.Length)];

                    cancellationToken.ThrowIfCancellationRequested();
                    _messenger.SendRawObject(instrument);
                }
            }
            catch (Exception e)
            {
                _logger.Error(e, "Fatal error in FileListener loop in directory '{directory}'");
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.Information("Stop file listener in directory '{directory}'", Path.GetFullPath(_config.Directory));
        cancellationTokenSource?.Cancel();
        cancellationTokenSource = null;
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        cancellationTokenSource?.Cancel();
        cancellationTokenSource = null;
    }
}
