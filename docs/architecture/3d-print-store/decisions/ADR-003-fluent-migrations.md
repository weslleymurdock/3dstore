# ADR-003: Database persistence and FluentMigrator strategy

## Context

Smartstore’s module documentation and existing Shipping migrations use FluentMigrator. Module entities are discovered by registering the module assembly with the SmartDbContext factory.

## Problem

Quotes, assets, analysis evidence, inventory movements, and jobs need relational integrity without changing Smartstore Core.

## Alternatives

1. EF Core migrations owned by the module.
2. Generic attributes/JSON only.
3. Module-owned entities, model configuration, and FluentMigrator schema migrations.

## Decision

Use `BaseEntity`-derived module entities and model configuration in the module assembly. Add versioned, defensive FluentMigrator migrations under `Migrations`; use foreign keys, indexes, status/concurrency fields, and audit records. Do not store binary files in relational columns and do not use EF migrations.

## Consequences

The module follows Smartstore database lifecycle and provider abstractions. Schema evolution needs migration tests for supported providers.

## Risks

Cross-provider DDL differences and module uninstall retention need careful policy. Initial uninstall should preserve business history unless an explicit purge is authorized.

## Open questions

Which retention and legal-hold rules determine when module records may be purged?
