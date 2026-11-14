using System;
using System.Linq;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;
using Smartstore.Caching;
using Smartstore.Collections;
using Smartstore.Core.Content.Menus;
using Smartstore.Core.Data;
using Smartstore.Core.Identity;
using Smartstore.Core.Localization;
using Smartstore.Core.Security;
using Smartstore.Data;
using Smartstore.Data.Providers;
using Smartstore.Engine;
using Smartstore.Engine.Modularity;
using Smartstore.Test.Common;
using Smartstore.ThreeDPrintStore;
using Smartstore.ThreeDPrintStore.Domain;

namespace Smartstore.Core.Tests.Platform.Modularity;

[TestFixture]
public class ThreeDPrintStoreBehaviorTests : ServiceTestBase
{
    private IPermissionService _permissionService;
    private Customer _administrator;
    private Customer _viewer;
    private Customer _guest;

    [OneTimeSetUp]
    public void InitializePermissionGraph()
    {
        var administratorRole = new CustomerRole { Id = 7101, Active = true, SystemName = "ThreeDPrintStoreAdministrators", Name = "3D Print Store Administrators" };
        var viewerRole = new CustomerRole { Id = 7102, Active = true, SystemName = "ThreeDPrintStoreViewers", Name = "3D Print Store Viewers" };
        var guestRole = new CustomerRole { Id = 7103, Active = true, SystemName = "ThreeDPrintStoreGuests", Name = "3D Print Store Guests" };

        _administrator = CreateCustomer(7101, "3D Print Store Administrator", administratorRole);
        _viewer = CreateCustomer(7102, "3D Print Store Viewer", viewerRole);
        _guest = CreateCustomer(7103, "3D Print Store Guest", guestRole);

        var self = new PermissionRecord { Id = 7201, SystemName = ThreeDPrintStorePermissions.Self };
        var read = new PermissionRecord { Id = 7202, SystemName = ThreeDPrintStorePermissions.Read };
        var update = new PermissionRecord { Id = 7203, SystemName = ThreeDPrintStorePermissions.Update };

        AddMapping(read, administratorRole, true);
        AddMapping(read, viewerRole, false);
        AddMapping(update, viewerRole, true);

        DbContext.PermissionRecords.AddRange(self, read, update);
        DbContext.SaveChanges();

        var localizationService = new Mock<ILocalizationService>().Object;
        var workContext = new Mock<IWorkContext>().Object;
        _permissionService = new PermissionService(
            DbContext,
            new Lazy<IWorkContext>(() => workContext),
            localizationService,
            NullCache.Instance);
    }

    [Test]
    public void DefaultPermissions_GrantReadWithoutGrantingUpdate()
    {
        var provider = new ThreeDPrintStorePermissionProvider();
        var administratorDefaults = provider.GetDefaultPermissions()
            .Single(x => x.CustomerRoleSystemName == SystemCustomerRoleNames.Administrators);
        var permissionNames = administratorDefaults.PermissionRecords.Select(x => x.SystemName).ToArray();

        Assert.That(permissionNames, Is.EqualTo(new[] { ThreeDPrintStorePermissions.Read }));
    }

    [Test]
    public void PermissionService_AdminCanReadButCannotUpdate()
    {
        Assert.That(_permissionService.Authorize(ThreeDPrintStorePermissions.Read, _administrator), Is.True);
        Assert.That(_permissionService.Authorize(ThreeDPrintStorePermissions.Update, _administrator), Is.False);
    }

    [Test]
    public void PermissionService_DeniesReadToCustomerWithoutReadPermission()
    {
        Assert.That(_permissionService.Authorize(ThreeDPrintStorePermissions.Read, _guest), Is.False);
    }

    [Test]
    public void PermissionService_DoesNotTreatUpdateAsReadPermission()
    {
        Assert.That(_permissionService.Authorize(ThreeDPrintStorePermissions.Read, _viewer), Is.False);
        Assert.That(_permissionService.Authorize(ThreeDPrintStorePermissions.Update, _viewer), Is.True);
    }

    [Test]
    public void AdminMenu_UsesReadPermissionForConfigurationEntry()
    {
        var root = new TreeNode<MenuItem>(new MenuItem { Id = "root" });
        root.Append(new MenuItem { Id = "modules" });

        new AdminMenu().BuildMenu(root);

        var menuItem = root
            .SelectNodeById("modules")
            .Children
            .Select(x => x.Value)
            .Single(x => x.ResKey == "Plugins.FriendlyName.Smartstore.ThreeDPrintStore");

        Assert.That(menuItem.PermissionNames, Is.EqualTo(ThreeDPrintStorePermissions.Read));
        Assert.That(_permissionService.Authorize(menuItem.PermissionNames, _administrator), Is.True);
        Assert.That(_permissionService.Authorize(menuItem.PermissionNames, _guest), Is.False);
    }

    [Test]
    public void ModuleAssembly_IsDiscoverableByTypeScanner()
    {
        var scanner = new DefaultTypeScanner(
            new[] { typeof(ModuleState).Assembly },
            new ModuleCatalog(Array.Empty<IModuleDescriptor>()),
            NullLogger.Instance);

        Assert.That(scanner.FindTypes<IModule>().Any(x => x.FullName == "Smartstore.ThreeDPrintStore.Module"), Is.True);
        Assert.That(scanner.FindTypes<IPermissionProvider>().Any(x => x == typeof(ThreeDPrintStorePermissionProvider)), Is.True);
    }

    [Test]
    public void ModuleModelAssembly_MapsModuleStateToExpectedTable()
    {
        var options = new DbContextOptionsBuilder<SmartDbContext>()
            .UseDbFactory(builder => builder.AddModelAssembly(typeof(ModuleState).Assembly))
            .Options;

        using var db = new SmartDbContext((DbContextOptions<SmartDbContext>)options);
        var entityType = db.Model.FindEntityType(typeof(ModuleState));

        Assert.Multiple(() =>
        {
            Assert.That(entityType, Is.Not.Null);
            Assert.That(entityType.GetTableName(), Is.EqualTo("ThreeDPrintStore_ModuleState"));
            Assert.That(db.ThreeDPrintStoreModuleStates(), Is.Not.Null);
        });
    }

    private Customer CreateCustomer(int id, string username, CustomerRole role)
    {
        var customer = new Customer { Id = id, Username = username };
        customer.CustomerRoleMappings.Add(new CustomerRoleMapping
        {
            CustomerId = id,
            CustomerRoleId = role.Id,
            CustomerRole = role
        });

        return customer;
    }

    private static void AddMapping(PermissionRecord permission, CustomerRole role, bool allow)
    {
        permission.PermissionRoleMappings.Add(new PermissionRoleMapping
        {
            CustomerRoleId = role.Id,
            CustomerRole = role,
            PermissionRecordId = permission.Id,
            PermissionRecord = permission,
            Allow = allow
        });
    }
}
