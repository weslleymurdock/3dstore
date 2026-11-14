using Microsoft.AspNetCore.Mvc;
using Smartstore.Core.Security;
using Smartstore.Web.Controllers;

namespace Smartstore.ThreeDPrintStore.Controllers;

/// <summary>
/// Hosts the minimal administration surface for the module foundation.
/// </summary>
public class ThreeDPrintStoreController : AdminController
{
    [Permission(ThreeDPrintStorePermissions.Read)]
    public IActionResult Configure()
    {
        return View();
    }
}
