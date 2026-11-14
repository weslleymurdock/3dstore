using Microsoft.EntityFrameworkCore;
using Smartstore.Core.Data;
using Smartstore.ThreeDPrintStore.Domain;

namespace Smartstore.ThreeDPrintStore;

/// <summary>
/// Provides access to the module's entity set through the Smartstore context.
/// </summary>
public static class SmartDbContextExtensions
{
    public static DbSet<ModuleState> ThreeDPrintStoreModuleStates(this SmartDbContext db)
        => db.Set<ModuleState>();
}
