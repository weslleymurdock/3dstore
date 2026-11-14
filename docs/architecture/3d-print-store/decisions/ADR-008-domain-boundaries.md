# ADR-008: Domain boundaries and aggregate strategy

## Context

The product spans customer files, analysis, quotes, materials, printers, production, inventory, and native commerce.

## Problem

Collapsing these concepts into products or generic attributes would lose lifecycle, ownership, auditability, and operational invariants.

## Alternatives

1. One large 3D aggregate.
2. Generic attributes attached to products/orders.
3. Bounded contexts with small aggregate roots and explicit integration records.

## Decision

Use ModelAssets, ModelAnalysis, Quoting, Materials, Printers, Production, Inventory, and CommerceIntegration contexts. Use identifiers and idempotent workflow records between contexts; keep each aggregate’s transactional boundary local.

## Consequences

The model is explicit and auditable but requires orchestration and reconciliation for cross-context workflows.

## Risks

Premature context coupling or distributed transactions could make retries unsafe. Keep Stage 02 infrastructure minimal.

## Open questions

Which operational facts require append-only event records versus current-state projections?
