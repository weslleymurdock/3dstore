# Domain model

## Bounded contexts

### ModelAssets

Owns logical assets, immutable versions, ownership, file fingerprints, processing state, validation findings, derivative references, and retention state. It does not own geometry calculations or prices.

### ModelAnalysis

Owns analysis requests/results and evidence: dimensions, volume, surface area, manifold status, support indicators, estimated material/time, engine identity, engine version, profile identity, and confidence. It exposes an `IModelAnalysisService` port and engine-specific adapters.

### Quoting

Owns quote lifecycle, selected asset version, selected material/profile, calculated line components, review decisions, customer response, expiry, and immutable accepted snapshot.

### Materials

Owns material families, grades, colors, density, nominal diameter, cost basis, and versioned material profiles. It does not own physical stock.

### Printers

Owns printer identity, capabilities, maintenance state, compatible materials, and printer/process profiles. A printer profile is not a material definition.

### Production

Owns production jobs, queue state, allocation, setup, execution, failure, waste, completion, and actual measurements. It references the accepted order/quote snapshot.

### Inventory

Owns spools and immutable stock movements in grams/meters, reservations, consumption, waste, correction, and reconciliation. Current balance is derived from movements or a controlled projection.

### CommerceIntegration

Owns the mapping between module concepts and native Smartstore identifiers, plus a durable snapshot link from quote/asset/analysis to `Product`, `ProductVariant`, `ShoppingCartItem`, `Order`, and `OrderItem`. It does not reimplement checkout, taxes, payment, or shipping.

## Aggregate candidates

| Aggregate | Root and consistency boundary | Key invariants |
| --- | --- | --- |
| Asset | `ModelAsset` | Asset has an owner; versions are immutable; only approved versions are quoteable. |
| Analysis | `AnalysisRun` | Result names its method/engine/profile and source version; terminal results are immutable. |
| Quote | `Quote` | Status transitions are legal and audited; accepted snapshot cannot be silently recalculated. |
| Material | `Material` | Unit, density, and compatibility facts are valid and versioned. |
| Spool | `Spool` | Quantity cannot be consumed below zero without an explicit correction policy. |
| Printer | `Printer` | Capability and availability are explicit; incompatible material/profile cannot be allocated. |
| Production job | `ProductionJob` | Allocation, start, pause, failure, completion, and waste transitions are audited. |
| Commerce link | `CommerceReference` | Native order association is idempotent and points to the accepted quote snapshot. |

These are design candidates, not entities for Stage 01. Cross-context operations use identifiers and durable application messages/workflow records instead of distributed transactions.

## State sketches

`AssetVersion`: `Uploaded -> Quarantined -> Validating -> Validated | Rejected -> Processing -> Ready | Failed -> Retained/Deleted`.

`Quote`: `Draft -> Submitted -> UnderReview -> Offered -> Accepted | Rejected | Expired | Withdrawn`.

`ProductionJob`: `Planned -> Queued -> Allocated -> Setup -> Printing -> Paused | Failed | Completed -> Verified`.

Transitions must be command-guarded and recorded with actor, reason, timestamp, and relevant input snapshot.

## Transaction boundaries

Relational writes for one aggregate and its audit records should be atomic within one `SmartDbContext` unit of work. Blob writes are not part of the database transaction: use staged storage, checksum verification, durable artifact state, and compensating cleanup. Quote acceptance and native order creation need an idempotency key and a reconciliation record because Smartstore checkout/order state and module state are separate boundaries.

## Units

Store canonical geometry dimensions in millimetres, mass in grams, filament length in millimetres, duration in seconds, and money in the native Smartstore currency/decimal conventions. Preserve original file-declared units and conversion evidence. Never infer a unit silently when the format is unitless; mark it unresolved and require a policy or customer confirmation.
