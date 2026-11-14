# ADR-007: Slicer runtime hierarchy and adapter architecture

## Context

The product requires a replaceable slicing mechanism but now has an explicit runtime priority: Bambu Studio first, PrusaSlicer second, and an Ender/Creality-compatible option third. Ender describes a printer family, not a slicer executable. Official sources confirm CLI surfaces for Bambu Studio, PrusaSlicer, and Creality Print, while also showing unresolved display, crash, profile, and distribution concerns.

## Problem

The module must select an engine compatible with a printer, material, process, and approved preset set. It must preserve provenance and never silently change manufacturing parameters during fallback.

## Alternatives

1. Couple the module directly to one slicer executable.
2. Treat any installed executable as interchangeable.
3. Use an engine descriptor/provider/adapter layer with explicit compatibility and ordered selection.

## Decision

Use an `IModelAnalysisService` boundary with a subordinate engine abstraction. The eventual implementation may use names such as `ISlicerEngineProvider`, `ISlicerEngineAdapter`, `SlicerEngineDescriptor`, and `SlicerEngineCapabilities`; exact interfaces are deferred to Stage 06.

The default ordered candidates are:

1. Bambu Studio adapter — primary.
2. PrusaSlicer adapter — secondary/fallback.
3. Creality Print adapter for Ender/Creality compatibility — tertiary. CuraEngine remains a future alternative candidate, not the default.

OrcaSlicer is not part of the official priority hierarchy and must not be a secondary fallback.

## Selection and fallback rules

1. Discover only deployment-approved engine installations/images, executable paths, version, platform, and capability declarations.
2. Build a compatibility decision from manufacturer/model, firmware/resource capabilities, material, nozzle/process profile, engine version, and preset checksums.
3. Try the ordered candidates only when the compatibility matrix explicitly permits the candidate.
4. A missing, disabled, incompatible, timed-out, or failed engine produces a diagnostic and a recorded attempt. It does not authorize changing printer, material, process, nozzle, or profile.
5. Fallback that changes any manufacturing characteristic requires explicit validation according to the operational policy and must be visible in the quote provenance.
6. If no compatible slicer is available, return a clearly labeled geometric/preliminary result or a blocked analysis state; never fabricate slicer precision.

## Version and provenance

Every result records engine family, engine version, adapter version, OS/image identity, manufacturer/model, firmware capability reference, printer-profile version/checksum, filament-profile version/checksum, process-profile version/checksum, effective parameters, source checksum, artifact checksums, attempt/fallback chain, warnings/errors, start/end time, duration, and estimate units/precision.

## Isolation

Only deployment-owned, allowlisted engines and arguments run in restricted workers. Uploaded binaries and G-code are never executed. Workers have bounded CPU, memory, time, disk, network, and filesystem permissions. Bambu Studio’s display requirement and Creality Print’s reported CLI crashes mean that separate OS/runtime-specific workers or virtual-display configurations may be required; one universal container is not assumed.

## Consequences

Bambu Studio receives first implementation and validation effort without making an unsupported production claim. PrusaSlicer provides a strong secondary path. Ender support is represented by a real slicing engine, initially Creality Print, while CuraEngine remains available for a later profile-focused evaluation.

## Risks

AGPL obligations, optional proprietary networking components, vendor profile redistribution, native dependencies, display/GPU requirements, nondeterminism, and version drift can affect commercial quoting and deployment.

## Open questions

- Can the approved Bambu Studio version run reliably in the intended no-display worker, with or without a virtual display, and preserve external presets?
- Which Bambu profiles/components may be redistributed or must be provisioned by the operator?
- Which Creality Print version produces stable CLI output for the supported Ender models?
- Should CuraEngine become the tertiary production path if Creality Print remains unstable, and how will its printer/material definitions be curated?
- What legal review is required for AGPL engines, modified workers, profiles, binaries, and network-facing use?
