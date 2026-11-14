# Smartstore integration map

## Confirmed host contracts

| Module need | Current repository evidence | Planned use |
| --- | --- | --- |
| Module loading | `dev-docs/compose/modules/getting-started-with-modules.md`; `Smartstore.Engine.Modularity.IModule` | `module.json` and `ModuleBase`; installation/uninstallation only module-owned data |
| DI and bootstrap | `Smartstore.DevTools/Startup.cs`, `Smartstore.Shipping/Startup.cs` | `StarterBase.ConfigureServices`; register ports and model assembly; defer expensive work |
| Module persistence | `Smartstore.Shipping/Startup.cs` uses `IDbContextConfigurationSource<SmartDbContext>` and `UseDbFactory` | Add module model assembly without changing core context source |
| Migrations | `dev-docs/framework/platform/database-migrations.md`; `Smartstore.Shipping/Migrations/*` | Versioned FluentMigrator classes; no EF migrations |
| Admin navigation | `Smartstore.DevTools/AdminMenu.cs` | Permission-protected module menu and submenus |
| ACL | `Smartstore.DevTools/Permissions.cs`; `IPermissionProvider` | Separate customer asset access, quote review, and production permissions |
| Localization | `ModuleBase.ImportLanguageResourcesAsync`, XML resources, manifest `ResourceRootKey` | `Localization/resources.en-us.xml` and `resources.de-de.xml` |
| Media | `IMediaService.SaveFileAsync`, `GetFileByIdAsync`, `StorageProvider`; `IMediaStorageProvider` | Original/derivative blob references via media; module metadata and status in relational records |
| Catalog | `Product`, `ProductVariant`, native product services | Predefined products remain entirely native; quote conversion uses a controlled native representation |
| Cart | `IShoppingCartService.AddToCartAsync`, `AddItemToCartAsync`, `SaveCartDataAsync` | Add only approved, priced quote representations; do not inject an unpriced upload into cart |
| Order | `IOrderProcessingService.PlaceOrderAsync`, order/item domain and `OrderPlacedEvent` | Native checkout/order creation; listen to order events for durable integration links |
| Output cache/UI | Existing module filters and bundle hooks | Add only where needed after route/view implementation; do not assume static CSS links |

## Proposed quote-to-order flow

1. The module validates that a quote is `Accepted`, unexpired, owned/authorized, and has an immutable price snapshot.
2. The module creates or resolves a native purchasable representation with a stable idempotency key. Exact product strategy is an open decision: dedicated product/variant, hidden product, or a supported custom-price path must be verified against current price/stock/tax behavior before implementation.
3. The module calls native cart behavior with the representation and records the quote/asset snapshot reference in module-owned integration data.
4. Native checkout remains responsible for customer, address, tax, shipping, discounts, payment, and order placement.
5. On `OrderPlacedEvent`, the module links the resulting `Order`/`OrderItem` and accepted quote snapshot. A retry-safe reconciliation task handles interrupted linking.

No direct insert into Smartstore order tables is proposed.

## Access model

Every read/download path checks customer ownership or admin permission. Public viewer URLs must reference approved, non-sensitive derivatives and use the media authorization model; original uploads and analysis reports remain private by default. Admin operations use explicit module permissions and, where entities are customer-scoped, ACL checks.

## Not an integration point

Generic attributes, product custom fields, or a single JSON blob are not the domain model for quotes, assets, spools, jobs, or audit history. They may be used only for small display metadata when the native feature is genuinely the correct owner.
