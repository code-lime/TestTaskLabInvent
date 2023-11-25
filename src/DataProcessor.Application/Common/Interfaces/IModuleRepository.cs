using DataProcessor.Domain.Entities;

namespace DataProcessor.Application.Common.Interfaces;

public interface IModuleRepository
{
    Task AddOrUpdateAsync(Module module, CancellationToken cancellationToken);
}
