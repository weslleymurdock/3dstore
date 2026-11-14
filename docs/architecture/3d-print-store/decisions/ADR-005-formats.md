# ADR-005: Supported 3D file formats

## Context

STL is ubiquitous but unitless and metadata-poor. 3MF carries richer package semantics. OBJ/MTL and AMF have useful capabilities but different external-reference and parser risks.

## Problem

Claiming support without parser, viewer, validation, and slicer evidence would create unsafe and inaccurate quotes.

## Alternatives

1. Accept every common extension.
2. Support STL only.
3. Capability-based support beginning with STL and 3MF, then controlled OBJ/MTL and AMF adapters.

## Decision

Use capability-based adapters. Initial proof candidates are STL and 3MF; OBJ/MTL and AMF remain conditional. Unit ambiguity, package limits, external references, and topology findings are explicit. G-code is output-only and never an upload input.

## Consequences

Format support grows from tested evidence rather than file-extension claims. A normalized immutable derivative enables viewer and analysis reuse.

## Risks

Different slicers interpret units, repairs, and multi-object packages differently. Engine-specific compatibility tests are required.

## Open questions

Which customer file corpus and format extensions must pass the acceptance suite?
