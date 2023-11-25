using DataProcessor.Application.Common.Interfaces;
using DataProcessor.Domain.Entities;
using Serilog;

namespace DataProcessor.Infrastructure.DataBase;

public class ModuleRepository : IModuleRepository
{
    private readonly ILogger _logger;
    private readonly IContext _context;

    public ModuleRepository(IContext context, ILogger logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task AddOrUpdateAsync(Module module, CancellationToken cancellationToken)
    {
        _logger.Debug("Begin AddOrUpdate transaction with module {moduleID}", module.ModuleCategoryID);

        using var transaction = await _context.DbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            Module? lastModule = await _context.Modules.FindAsync([ module.ModuleCategoryID ], cancellationToken);

            if (lastModule is null) await _context.Modules.AddAsync(module, cancellationToken);
            else lastModule.ModuleState = module.ModuleState;

            await _context.DbContext.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception e)
        {
            _logger.Error(e, "Error in AddOrUpdate transaction with module {moduleID}", module.ModuleCategoryID);
            return;
        }
        _logger.Debug("End AddOrUpdate transaction with module {moduleID}", module.ModuleCategoryID);
    }
}
