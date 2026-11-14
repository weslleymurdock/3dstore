using System.Reflection;
using System.Text.Json;
using NUnit.Framework;
using Smartstore.Core.Security;
using Smartstore.Domain;
using Smartstore.Engine.Modularity;
using Smartstore.ThreeDPrintStore;
using Smartstore.ThreeDPrintStore.Domain;

namespace Smartstore.Core.Tests.Platform.Modularity;

[TestFixture]
public class ThreeDPrintStoreModuleTests
{
    private static string ModuleRoot => Path.GetFullPath(Path.Combine(
        TestContext.CurrentContext.TestDirectory,
        "../../../../..",
        "src/Smartstore.Modules/Smartstore.ThreeDPrintStore"));

    [Test]
    public void ManifestHasExpectedIdentityAndCompatibility()
    {
        using var document = JsonDocument.Parse(File.ReadAllText(Path.Combine(ModuleRoot, "module.json")));
        var root = document.RootElement;

        Assert.Multiple(() =>
        {
            Assert.That(root.GetProperty("SystemName").GetString(), Is.EqualTo("Smartstore.ThreeDPrintStore"));
            Assert.That(root.GetProperty("Version").GetString(), Is.EqualTo("6.4.0"));
            Assert.That(root.GetProperty("MinAppVersion").GetString(), Is.EqualTo("6.4.0"));
        });
    }

    [Test]
    public void ModuleAssemblyContainsLifecycleAndStartupContracts()
    {
        var assembly = typeof(ModuleState).Assembly;
        var moduleType = assembly.GetType("Smartstore.ThreeDPrintStore.Module");
        var startupType = assembly.GetType("Smartstore.ThreeDPrintStore.Startup");

        Assert.Multiple(() =>
        {
            Assert.That(moduleType, Is.Not.Null);
            Assert.That(typeof(IModule).IsAssignableFrom(moduleType), Is.True);
            Assert.That(startupType, Is.Not.Null);
            Assert.That(startupType.GetMethod("ConfigureServices"), Is.Not.Null);
        });
    }

    [Test]
    public void PermissionProviderExposesOnlyFoundationPermissions()
    {
        var provider = new ThreeDPrintStorePermissionProvider();
        var permissions = provider.GetPermissions().Select(x => x.SystemName).ToArray();

        Assert.That(permissions, Is.EquivalentTo(new[]
        {
            ThreeDPrintStorePermissions.Self,
            ThreeDPrintStorePermissions.Read,
            ThreeDPrintStorePermissions.Update
        }));
    }

    [Test]
    public void ModuleStateIsAHostEntityAndMigrationIsDiscoverable()
    {
        var migrationType = typeof(ModuleState).Assembly.GetType("Smartstore.ThreeDPrintStore.Migrations.Initial");

        Assert.Multiple(() =>
        {
            Assert.That(typeof(BaseEntity).IsAssignableFrom(typeof(ModuleState)), Is.True);
            Assert.That(migrationType, Is.Not.Null);
            Assert.That(migrationType.GetCustomAttributes().Any(x => x.GetType().Name == "MigrationVersionAttribute"), Is.True);
        });
    }

    [TestCase("resources.en-us.xml")]
    [TestCase("resources.de-de.xml")]
    public void LocalizationResourceFileIsPresent(string fileName)
    {
        var path = Path.Combine(ModuleRoot, "Localization", fileName);

        Assert.That(File.Exists(path), Is.True);
        Assert.That(File.ReadAllText(path), Does.Contain("Configuration.Installed"));
    }
}
