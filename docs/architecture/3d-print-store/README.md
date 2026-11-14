# 3D Print Store Architecture

## Status and scope

This document records Stage 01 discovery for the proposed independent module `Smartstore.ThreeDPrintStore`. It is architecture documentation only. No module project, production entity, migration, controller, endpoint, client dependency, or Smartstore core change is introduced by this stage.

The repository was inspected on 2026-11-14 on branch `feat/3d-store`; the worktree was clean. The host is Smartstore 6.4.0 and the central target is `net11.0`. The installed SDK/runtime is 11.0.100. There is no `global.json` and no root `Directory.Packages.props`; existing package versions are therefore left untouched.

## Architectural position

The module owns 3D assets, analysis, quotations, print-operation master data, production planning, and traceability. Smartstore remains authoritative for catalog products, customers, carts, checkout, tax, shipping, orders, media abstractions, permissions, localization, and module loading.

The custom-print flow is deliberately quote-first: an upload is not a cart item and has no final price until validation, analysis, material/profile selection, and administrative approval produce a price snapshot. After customer acceptance, a later implementation will create a native purchasable catalog representation and use the existing cart/checkout/order pipeline.

## Confirmed Smartstore extension points

The current source and `dev-docs/` confirm these integration points:

| Concern | Confirmed contract or convention | Stage 01 position |
| --- | --- | --- |
| Module identity | `module.json`, `Module.cs`, `ModuleBase`, `IModule` | Assembly and root namespace `Smartstore.ThreeDPrintStore`; `ModuleBase` lifecycle |
| Startup and DI | `Startup : StarterBase`, `ConfigureServices`, pipeline/route hooks | Register module services and model assembly only when implementation begins |
| Persistence | `IDbContextConfigurationSource<SmartDbContext>` plus `UseDbFactory(...AddModelAssembly(...))` | Module-owned EF model assembly; schema changes through FluentMigrator |
| Admin navigation | `AdminMenuProvider` | Add a permission-protected module branch |
| Permissions | `IPermissionProvider`, `PermissionRecord`, `PermissionAttribute`/admin authorization | Separate upload/customer and operational/admin permissions |
| Media | `IMediaService`, `IMediaStorageProvider`, transient media files, metadata and folders | Use media/storage abstractions for blobs; keep domain metadata in module tables |
| Catalog/cart | `Product`, `ProductVariant`, `IShoppingCartService` | Reuse native catalog and cart for predefined products and accepted quotes |
| Orders | `Order`, `OrderItem`, `IOrderProcessingService`, `OrderPlacedEvent` | Attach immutable quote/model references through module-owned integration records |
| Localization | `ResourceRootKey`, XML resources, `ImportLanguageResourcesAsync` | English and German module resources |
| Packaging | `OutputPath` into `src/Smartstore.Web/Modules/`, manifest version and `MinAppVersion` | Never edit the generated Web/Modules copy |

## Proposed module shape

The future project is a Razor class library under `src/Smartstore.Modules/Smartstore.ThreeDPrintStore/`, targeting the same `net11.0` TFM imported by `src/Smartstore.Modules/Directory.Build.props`. Suggested internal areas are `Domain`, `Services`, `Providers`, `Controllers`, `Models`, `Migrations`, `Media`, `Security`, `Tasks`, `Views`, `Localization`, and `wwwroot`. The project must not be created in Stage 01.

The module should remain a modular monolith initially. Long-running analysis and slicing are isolated behind ports and may later be moved to a worker process without changing the domain contracts.

## Delivery sequence

1. Stage 02 creates the module shell, installation lifecycle, permissions, model assembly registration, migration infrastructure, and health-safe empty module.
2. Stage 03 adds material, printer, profile, and administrative master data.
3. Stage 04 adds upload, quarantine, storage, checksum, validation, and derivative lifecycle.
4. Stage 05 adds a locally packaged viewer integration after dependency/licensing approval.
5. Stage 06 adds format adapters, geometric analysis, and isolated slicer adapters.
6. Stage 07 adds pricing and quote snapshots.
7. Stage 08 integrates approval with native cart/checkout/order behavior.
8. Stages 09–12 add production, inventory, operations, reporting, security hardening, and release validation.

## Evidence boundary

The recommendations are grounded in the repository sources and mandatory `dev-docs/` pages. External references used for technology evaluation are linked in [technology-evaluation.md](./technology-evaluation.md). Parser/slicer package selection remains open until a proof of concept verifies format fidelity, license compatibility, sandboxing, and deployment behavior.

## Stage 01 acceptance statement

The architecture is executable as a plan for Stage 02 without speculative extension points. The roadmap is not changed because discovery did not invalidate the existing stage separation. Business policies such as margin, machine-hour rate, tolerance, deposits, quote expiry, cancellation, and retention duration remain explicit open decisions.
