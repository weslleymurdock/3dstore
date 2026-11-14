# ADR-001: Module architecture and Smartstore integration

## Context

Smartstore discovers Razor modules through `module.json`, `Module.cs`, and starter classes. Existing DevTools, PayPal, Shipping, and OfflinePayment modules use `ModuleBase`; Shipping registers its model assembly through `IDbContextConfigurationSource<SmartDbContext>`.

## Problem

3D printing needs domain, UI, storage, processing, and operational features without forking Smartstore core.

## Alternatives

1. Modify Smartstore Core/Web directly.
2. Build a separate application beside Smartstore.
3. Build an independent installable module with ports for workers.

## Decision

Use `src/Smartstore.Modules/Smartstore.ThreeDPrintStore/`, assembly/root namespace `Smartstore.ThreeDPrintStore`, `ModuleBase`, `Startup : StarterBase`, module-owned persistence and permissions, and native Smartstore services for commerce.

## Consequences

Features remain installable and can evolve without core changes. Module APIs must respect current host contracts and provider/database capabilities.

## Risks

Some native checkout or custom-price behavior may not expose a suitable extension point. Prove the accepted-quote conversion path before Stage 08.

## Open questions

Which native product/variant strategy is acceptable for a priced custom quote?
