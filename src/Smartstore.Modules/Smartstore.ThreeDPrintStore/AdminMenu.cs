using Smartstore.Collections;
using Smartstore.Core.Content.Menus;
using Smartstore.Web.Rendering.Builders;

namespace Smartstore.ThreeDPrintStore;

/// <summary>
/// Adds the 3D Print Store configuration entry to the administration menu.
/// </summary>
public class AdminMenu : AdminMenuProvider
{
    protected override void BuildMenuCore(TreeNode<MenuItem> modulesNode)
    {
        modulesNode.Append(new MenuItem().ToBuilder()
            .Text("3D Print Store")
            .ResKey("Plugins.FriendlyName.Smartstore.ThreeDPrintStore")
            .Icon("cube")
            .PermissionNames(ThreeDPrintStorePermissions.Read)
            .Action("Configure", "ThreeDPrintStore", new { area = "Admin" })
            .AsItem());
    }
}
