# ADR-007: Slicer integration architecture

## Context

PrusaSlicer and OrcaSlicer offer CLI/headless workflows but are native processes with AGPL licensing and profile/version sensitivity.

## Problem

Quotes need reliable estimates without executing untrusted files or presenting heuristics as slicer results.

## Alternatives

1. In-process managed slicing library.
2. Direct execution of a user-provided slicer/G-code.
3. `IModelAnalysisService` with approved engine adapters in an isolated worker.

## Decision

Use a port with geometric and engine-specific implementations. Pin engine/profile versions, isolate the worker, apply resource limits, capture diagnostics, record provenance, and never execute uploaded G-code or binaries.

## Consequences

The web process remains safer and responsive. Deployment must operate a worker and maintain approved engine images/profiles.

## Risks

AGPL obligations, native vulnerabilities, output nondeterminism, and profile drift can affect commercial quoting.

## Open questions

Which engine/license/deployment model is legally and operationally approved?
