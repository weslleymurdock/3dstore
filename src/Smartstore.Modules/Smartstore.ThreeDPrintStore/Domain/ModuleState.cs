using System;
using System.ComponentModel.DataAnnotations.Schema;
using Smartstore.Domain;

namespace Smartstore.ThreeDPrintStore.Domain;

/// <summary>
/// Stores the technical state owned by the module foundation.
/// </summary>
[Table("ThreeDPrintStore_ModuleState")]
public class ModuleState : BaseEntity
{
    public DateTime CreatedOnUtc { get; set; }
}
