global using System;
global using System.Collections.Generic;
global using System.Threading.Tasks;
global using Smartstore.Engine.Modularity;
global using Smartstore.Web.Modelling;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Smartstore.Http;

namespace Smartstore.ThreeDPrintStore;

internal class Module : ModuleBase, IConfigurable
{
    public ILogger Logger { get; set; } = NullLogger.Instance;

    public RouteInfo GetConfigurationRoute()
        => new("Configure", "ThreeDPrintStore", new { area = "Admin" });

    public override async Task InstallAsync(ModuleInstallationContext context)
    {
        await ImportLanguageResourcesAsync();
        await base.InstallAsync(context);
    }

    public override async Task UninstallAsync()
    {
        // The module's schema is intentionally preserved so a reinstall does not discard data.
        await DeleteLanguageResourcesAsync();
        await base.UninstallAsync();
    }
}
