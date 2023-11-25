using DataProcessor.Configs;
using DataProcessor.Infrastructure;
using DataProcessor.Interfaces;
using DataProcessor.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", Serilog.Events.LogEventLevel.Warning)
    .WriteTo.Console()
    .CreateLogger();

builder.Host
    .UseSerilog();

builder.Services
    .AddSingleton(Log.Logger)
    .AddLogging(v => v.AddSerilog(dispose: true));

builder.Services
    .Configure<RabbitMqConfig>(builder.Configuration.GetSection(RabbitMqConfig.SectionKey));

builder.Services
    .AddInfrastructureServices(builder.Configuration)
    .AddScoped<IDataProcessor, DataProcessorService>()
    .AddHostedService<RabbitMqReceiverService>();

var app = builder.Build();

app.Run();