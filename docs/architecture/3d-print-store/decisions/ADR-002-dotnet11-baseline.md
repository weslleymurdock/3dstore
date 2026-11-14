# ADR-002: .NET 11 compatibility and dependency baseline

## Context

`src/Smartstore.Build/Smartstore.Common.props` sets `TargetFramework` to `net11.0`. The installed SDK/runtime is 11.0.100/11.0.0. Modules import this shared property through `src/Smartstore.Modules/Directory.Build.props`. No `global.json` or root package central-management file is present.

## Problem

The module must load into the host process without changing the repository baseline or introducing speculative package upgrades.

## Alternatives

1. Target a different framework in the module.
2. Migrate the repository as part of this stage.
3. Target the shared host TFM and preserve package versions.

## Decision

Target the host’s shared `net11.0` configuration. Do not change SDK, TFMs, package versions, or build configuration in Stage 01. Validate compatibility by compiling the future module against the live solution.

## Consequences

The module shares the host runtime and assembly contracts. Package age is not treated as incompatibility without a build/runtime finding.

## Risks

Native slicer/parser components may have platform-specific support independent of .NET. Keep them behind deployment-tested adapters.

## Open questions

Whether production deployment will require a pinned SDK policy or worker-specific runtime image.
