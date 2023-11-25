using DataProcessor.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataProcessor.Application.Common.Interfaces;

public interface IContext
{
    DbSet<Module> Modules { get; }
    DbContext DbContext { get; }
}