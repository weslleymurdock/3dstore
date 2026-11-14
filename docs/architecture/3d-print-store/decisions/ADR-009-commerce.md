# ADR-009: Quotation and native commerce integration

## Context

Smartstore already owns products, variants, cart, checkout, pricing, taxes, shipping, orders, and payment. The module owns a customer-specific quote and its model/analysis evidence.

## Problem

An accepted quote must become purchasable without bypassing native commerce rules or losing the accepted snapshot.

## Alternatives

1. Insert an order directly from the module.
2. Keep the quote outside native checkout and invoice separately.
3. Create a native purchasable representation and use native cart/checkout/order services with an idempotent module link.

## Decision

Choose option 3 as the target architecture. Do not directly insert order rows. The exact product/variant/custom-price mechanism is an implementation gate and remains open until verified against current Smartstore pricing, tax, stock, and checkout contracts.

## Consequences

Customers receive native checkout behavior and operators receive native orders. Quote, asset, analysis, and price snapshots remain module-owned and linked to native identifiers.

## Risks

Native price recalculation or product visibility may conflict with a quote price. Revalidate the snapshot at cart/order boundaries and provide reconciliation for interrupted conversion.

## Open questions

Which supported native representation preserves customer-specific price and tax semantics?
