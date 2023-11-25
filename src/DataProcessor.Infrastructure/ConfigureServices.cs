using DataProcessor.Application.Common.Interfaces;
using DataProcessor.Infrastructure.DataBase;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataProcessor.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        string file = configuration.GetRequiredSection("SQLite").GetRequiredSection("File").Get<string>()!;

        SqliteConnectionStringBuilder builder = new SqliteConnectionStringBuilder()
        {
            DataSource = file
        };

        return services
            .AddDbContext<IContext, ApplicationDbContext>(options => options.UseSqlite(builder.ConnectionString))
            .AddScoped<IModuleRepository, ModuleRepository>();
    }
}