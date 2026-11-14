# Technology evaluation

## Viewer

Three.js is the recommended viewer foundation for Stage 05, subject to local packaging and bundle-size review. Its official documentation provides loaders, cameras, controls, WebGL/WebGPU renderers, and examples; it is not a geometry validator, slicer, or production estimator. The project license is MIT according to the official repository license: <https://github.com/mrdoob/three.js/blob/dev/LICENSE>. The official docs are at <https://threejs.org/docs/>.

The viewer should consume a server-produced, authorization-checked derivative. Initial features are orbit controls, fit-to-bounds, units/dimensions display, error fallback, and accessible status text. Thumbnail generation belongs to the server pipeline. Mobile performance requires triangle/texture budgets, progressive loading, and a non-WebGL fallback.

No client-side dependency is installed in Stage 01, and a public CDN is not a runtime requirement.

## Slicer candidates

| Candidate | Strengths | Constraints | Stage 01 decision |
| --- | --- | --- | --- |
| PrusaSlicer | Mature CLI, broad printer/material ecosystem, established profiles | AGPL obligations, native process/resource isolation, profile compatibility | Primary proof-of-concept candidate |
| OrcaSlicer | Modern profiles and calibration-oriented workflow, CLI lineage | AGPL obligations, native dependencies, profile/engine drift | Secondary comparison candidate |
| Custom geometric estimator | No external process; deterministic and portable | Not a slicer; cannot promise real print time/material | Required fallback, clearly labeled heuristic |

Official references reviewed by read-only HTTP checks on 2026-11-14: PrusaSlicer CLI <https://github.com/prusa3d/PrusaSlicer/wiki/Command-Line-Interface>, PrusaSlicer license <https://github.com/prusa3d/PrusaSlicer/blob/master/LICENSE>, and OrcaSlicer license <https://github.com/SoftFever/OrcaSlicer/blob/main/LICENSE.txt>. Legal review is still required before redistribution or hosted execution.

## Format/specification references

The 3MF Consortium specification is the preferred reference for 3MF package semantics: <https://3mf.io/specification/>. STL, OBJ/MTL, and AMF support remains adapter-based and must be validated against real sample corpora and slicer behavior. The architecture deliberately avoids choosing a parsing package before a license and security review.

## Storage

Smartstore's `IMediaService` and `IMediaStorageProvider` are the first integration choice because they already abstract storage, media metadata, transient files, URLs, and provider capabilities. Module tables hold ownership, checksums, artifact roles, processing state, and retention references. A dedicated object store provider may be evaluated later if the deployed media provider cannot meet large-file streaming, isolation, or lifecycle requirements.

## Compatibility findings

The host and module build imports resolve to `net11.0`; the installed SDK and ASP.NET Core runtime are 11.0.0. Existing module package versions, including MiniProfiler 4.5.4 in DevTools, remain unchanged. No compatibility failure was observed during discovery, but Stage 02 must compile the empty module against the live solution and must not infer compatibility from package age alone.
