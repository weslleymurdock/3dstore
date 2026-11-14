# ADR-006: 3D visualization technology

## Context

The product needs browser visualization on desktop and mobile. Three.js provides loaders and rendering but is not a parser, validator, or estimator.

## Problem

The viewer must be maintainable, locally deployable, and safe for customer files without turning client rendering into a trust boundary.

## Alternatives

1. Three.js packaged with the module.
2. A hosted CDN viewer dependency.
3. Server-rendered images only.

## Decision

Evaluate and initially plan for locally packaged Three.js, consuming approved server derivatives. CDN availability is not required. Provide fit-to-bounds, dimensions/status, mobile budgets, error fallback, and accessible status text.

## Consequences

Bundle and license review is owned by the module release. Server validation remains authoritative.

## Risks

Large meshes can exhaust mobile GPU/memory. Enforce derivative triangle/texture budgets and progressive loading.

## Open questions

Which Three.js version and loader subset will be approved after bundle/security review?
