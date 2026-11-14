# Requirements

## Functional requirements

| ID | Requirement | Priority | Boundary |
| --- | --- | --- | --- |
| FR-01 | Sell predefined printed products through the native Smartstore catalog. | Must | Smartstore catalog/cart/checkout |
| FR-02 | Accept an authenticated or explicitly policy-approved customer 3D upload. | Must | Module upload service + media storage |
| FR-03 | Validate extension, detected format, size, structural integrity, units, dimensions, and safety limits. | Must | Module pipeline |
| FR-04 | Track immutable asset versions and derived artifacts. | Must | ModelAssets |
| FR-05 | Provide a browser viewer for approved derivatives, with Three.js evaluated as the initial candidate. | Should | Storefront/admin UI |
| FR-06 | Analyze geometry and distinguish heuristic, geometric, and slicer-produced estimates. | Must | ModelAnalysis |
| FR-07 | Maintain materials, colors, profiles, printers, compatibility, and production settings. | Must | Materials/Printers |
| FR-08 | Create revisable quotations with explicit status transitions and immutable price snapshots. | Must | Quoting |
| FR-09 | Allow administrative review and customer acceptance/rejection. | Must | Quoting + ACL |
| FR-10 | Convert an accepted quote to a native Smartstore order without bypassing checkout rules. | Must | CommerceIntegration |
| FR-11 | Plan production, allocate printers, record actual duration, material use, waste, failures, and maintenance. | Should | Production/Inventory |
| FR-12 | Provide operational dashboards and profitability reports from auditable facts. | Could | Operations |

## Non-functional requirements

- Security: uploads are untrusted; no uploaded binary or G-code is executed. Processing uses quotas, timeouts, memory/CPU limits, isolated working directories, allowlists, and least-privilege identities.
- Traceability: every estimate records source asset version, analysis method, engine/version, profile, inputs, timestamp, and confidence/quality state.
- Durability: relational records contain references and facts, not large 3D blobs. Blob storage remains behind Smartstore media/storage abstractions.
- Availability: upload and analysis are asynchronous; a user can see durable state and retryable failure rather than holding an HTTP request open.
- Portability: module code follows Smartstore's supported database providers and `net11.0` host TFM; slicer adapters are capability-declared by OS/container deployment.
- Privacy: customer ownership and authorization are checked for every asset, derivative, report, and download; retention and deletion are policy-driven.
- Accessibility: storefront/admin UI follows existing Smartstore accessibility and localization conventions.
- Performance: constructors remain cheap; expensive parsing, thumbnail generation, and slicing are deferred to queued work.

## Glossary

| Term | Meaning |
| --- | --- |
| Asset | A logical customer or catalog 3D model with one or more immutable versions. |
| Artifact | A file derived from an asset version, such as a normalized mesh, thumbnail, report, or slicer output. |
| Quote | A versioned commercial proposal containing selected inputs and a price snapshot. |
| Estimate | A result produced by a named method; never implies physical accuracy unless supported by evidence. |
| Print profile | A versioned set of printer/material/process parameters. |
| Spool | A traceable physical inventory item, distinct from a material definition. |
| Production job | The operational work created from an accepted native order. |

## Explicitly unresolved policies

The implementation must obtain decisions for pricing formula and margin, minimum charge, labor and machine rates, acceptable geometry tolerances, quote expiry, customer cancellation/refund rules, anonymous upload policy, storage retention, maximum dimensions, allowed materials, production priority, reprint policy, and data residency.
