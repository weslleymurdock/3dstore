using System.Collections.Generic;
using System.Linq;
using Smartstore.Core.Identity;
using Smartstore.Core.Security;

namespace Smartstore.ThreeDPrintStore;

/// <summary>
/// Defines the permissions exposed by the 3D Print Store module.
/// </summary>
public static class ThreeDPrintStorePermissions
{
    public const string Self = "threedprintstore";
    public const string Read = "threedprintstore.read";
    public const string Update = "threedprintstore.update";
}

/// <summary>
/// Provides the administrative permissions for the 3D Print Store module.
/// </summary>
public sealed class ThreeDPrintStorePermissionProvider : IPermissionProvider
{
    public IEnumerable<PermissionRecord> GetPermissions()
    {
        var permissionSystemNames = PermissionHelper.GetPermissions(typeof(ThreeDPrintStorePermissions));
        return permissionSystemNames.Select(x => new PermissionRecord { SystemName = x });
    }

    public IEnumerable<DefaultPermissionRecord> GetDefaultPermissions()
    {
        return new[]
        {
            new DefaultPermissionRecord
            {
                CustomerRoleSystemName = SystemCustomerRoleNames.Administrators,
                PermissionRecords = new[]
                {
                    new PermissionRecord { SystemName = ThreeDPrintStorePermissions.Read }
                }
            }
        };
    }
}
