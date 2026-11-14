# Security model

## Threat model

The attacker may be an unauthenticated uploader, a customer attempting cross-account access, a malicious administrator or compromised worker, or a crafted 3D/package file. Threats include malware delivery, parser memory exhaustion, decompression bombs, path traversal, SSRF through external references, unauthorized model disclosure, command injection, G-code execution, quota exhaustion, and price/order tampering.

## Required controls

- Require authentication for uploads unless an explicit anti-abuse policy permits anonymous intake; bind every asset to an owner and store.
- Enforce byte, archive, entry, triangle, object, dimension, time, memory, and derivative quotas before and during processing.
- Use content sniffing plus extension allowlists; never trust the client MIME type.
- Store originals in quarantine until validation completes. Use generated opaque names and Smartstore media/storage APIs; never concatenate user paths.
- Disable XML external entities and network resolution; reject external OBJ/MTL/texture references unless safely included and approved by the adapter.
- Run parsers and slicers in a least-privilege isolated worker with no unnecessary network, filesystem, or process permissions.
- Never execute uploaded executables or G-code. Only invoke an allowlisted, deployment-owned engine path with fixed arguments.
- Serve originals, reports, and slicer outputs only through authorization checks. Public viewers use approved derivatives.
- Record actor, asset version, checksum, status transition, policy decision, and admin override in audit records.
- Use idempotency and optimistic concurrency for quote acceptance and order conversion; do not trust client-side price totals.
- Apply Smartstore anti-forgery, authorization, ACL, and localization conventions to module controllers/views.
- Scan dependencies and generated outputs; define retention/erasure behavior for customer data, backups, and derived artifacts.

## Data classification

| Data | Default classification | Access |
| --- | --- | --- |
| Original customer model | Private customer data | Owner, authorized staff, controlled service |
| Validated/normalized model | Private operational data | Owner, authorized staff, workers |
| Thumbnail/viewer derivative | Restricted; public only by policy | Owner or approved quote/product audience |
| Analysis report | Private commercial/technical data | Owner, reviewers, production staff |
| Slicer output | Restricted operational data | Production staff and isolated worker |
| Quote price snapshot | Confidential commercial data | Owner and authorized reviewers |

## Retention and recovery

Retention periods, legal holds, backup deletion, and customer erasure are open policy decisions. The implementation must distinguish logical deletion from physical blob deletion, maintain references until dependent orders/jobs no longer need them, and make cleanup observable and retryable.
