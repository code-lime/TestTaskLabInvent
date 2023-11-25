using FileParser.Common.Configs;
using FileParser.Interfaces;
using FileParser.Services;
using Microsoft.Extensions.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .WriteTo.Console()
    .CreateLogger();

builder.Logging
    .ClearProviders()
    .AddConsole();

builder.Host
    .UseSerilog();
builder.Services
    .AddSingleton(Log.Logger)
    .AddLogging(v => v.AddSerilog(dispose: true));

builder.Services
    .Configure<FileListenerConfig>(builder.Configuration.GetSection(FileListenerConfig.SectionKey))
    .Configure<RabbitMqConfig>(builder.Configuration.GetSection(RabbitMqConfig.SectionKey));

builder.Services
    .AddScoped<IQuerySender, RabbitMqSenderService>()
    .AddSingleton<IFileParser, FileParserService>()
    .AddHostedService<FileListenerService>();

var app = builder.Build();

app.Run();