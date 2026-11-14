using FluentMigrator;
using Smartstore.Core.Data.Migrations;
using Smartstore.ThreeDPrintStore.Domain;

namespace Smartstore.ThreeDPrintStore.Migrations;

[MigrationVersion("2026-11-14 09:00:00", "3D Print Store: Initial")]
internal sealed class Initial : Migration
{
    public override void Up()
    {
        const string tableName = "ThreeDPrintStore_ModuleState";

        if (!Schema.Table(tableName).Exists())
        {
            Create.Table(tableName)
                .WithIdColumn()
                .WithColumn(nameof(ModuleState.CreatedOnUtc)).AsDateTime2().NotNullable();
        }
    }

    public override void Down()
    {
        // Preserve module data when the module is uninstalled.
    }
}
