# Technology evaluation

## Evidence policy

This evaluation was refreshed on 2026-11-14. “Confirmed” means described by an official project source. “Observed” means reported by an official issue or repository behavior, not independently reproduced here. “Not validated” means a local proof of concept is still required. No runtime, binary, profile, NuGet package, or worker was installed by this refinement.

Official sources consulted on 2026-11-14: [Bambu Studio CLI](https://github.com/bambulab/BambuStudio/wiki/Command-Line-Usage), [Bambu Studio repository/license](https://github.com/bambulab/BambuStudio), [Bambu headless issue](https://github.com/bambulab/BambuStudio/issues/8293), [PrusaSlicer CLI](https://github.com/prusa3d/PrusaSlicer/wiki/Command-Line-Interface), [PrusaSlicer license](https://github.com/prusa3d/PrusaSlicer/blob/master/LICENSE), [Creality Print repository/CLI](https://github.com/CrealityOfficial/CrealityPrint), [Creality Print CLI issue](https://github.com/CrealityOfficial/CrealityPrint/issues/574), [CuraEngine repository](https://github.com/Ultimaker/CuraEngine), [Three.js documentation](https://threejs.org/docs/), and [Three.js license](https://github.com/mrdoob/three.js/blob/dev/LICENSE).

## Official priority

The product priority is fixed as:

1. Bambu Studio — primary runtime.
2. PrusaSlicer — secondary runtime and fallback.
3. Ender/Creality compatibility — tertiary slicing option, represented initially by Creality Print; CuraEngine remains a future alternative candidate.

Ender is a printer family/ecosystem, not an executable. The adapter model therefore selects a slicing engine by printer capability and approved profile compatibility.

## Comparative matrix

| Criterion | Bambu Studio | PrusaSlicer | Ender/Creality option |
| --- | --- | --- | --- |
| Priority | **Primary; product decision** | **Secondary; product decision** | **Tertiary; product decision** |
| CLI/headless | **CLI confirmed** by official Command Line Usage; true no-display server mode **not validated**. Official issue evidence reports Wayland/display initialization failures. | **CLI confirmed** by official CLI documentation; no-display/container behavior **not validated here**. | **CLI confirmed** by Creality Print README; official issue evidence reports crashes in some CLI scenarios. CuraEngine is a standalone console engine, but is not yet the selected Ender default. |
| Linux execution | Linux distribution/AppImage references are present in official Bambu sources; server execution **not validated**. | Linux support is documented by the project; worker image **not validated**. | Creality Print repository documents Ubuntu build/dependencies and Linux AppImage distribution; worker execution **not validated**. |
| Containerization | **Not validated**; display/GPU/runtime dependencies must be proven. | **Not validated**; native dependencies and profiles must be packaged and tested. | **Not validated**; Creality Print may require GUI/runtime libraries; CuraEngine is more naturally worker-oriented but profile integration remains open. |
| Printer profiles | CLI supports loading machine/process settings; compatibility with selected Bambu printer/profile is **not validated**. | Profile loading from AMF/3MF and CLI overrides are officially documented. | Creality Print CLI documents printer/process setting loading; Ender model coverage and preset compatibility are **not validated**. |
| Filament profiles | CLI supports loading filament settings; multi-slot mapping requires proof. | Material presets are supported by the profile system; exact worker contract is **not validated**. | Creality Print documents `--load_filaments`; exact Ender profile coverage is **not validated**. |
| Time estimate | Slicedata/time output is reported in official issue evidence; output semantics and accuracy are **not validated**. | Slicer output is suitable for an estimate; exact extraction contract is **not validated**. | CLI result files/metadata are documented, but time extraction for target Ender profiles is **not validated**. |
| Material estimate | Weight/material fields are reported in official issue evidence, including a failure case with zero grams; correctness is **not validated**. | **Not validated** for the module’s extraction contract. | **Not validated** for target Ender profiles. |
| G-code/export | CLI documents output directory and 3MF/slicedata export; exact G-code artifact contract is **not validated**. | G-code export is established CLI behavior; module extraction is **not validated**. | README documents generated G-code/result files; module extraction is **not validated**. |
| Licensing | Official repository identifies AGPL-3.0; optional non-free networking plugin is called out by the project. Distribution and server obligations require legal review. | AGPL-3.0; distribution and server obligations require legal review. | Creality Print repository contains license files; exact terms for binaries, profiles, and commercial redistribution require legal review. CuraEngine is AGPL-3.0. |
| Commercial use | Technically possible under license terms, but no legal approval exists for this project. | Same. | Same; vendor profiles, binaries, and trademarks need separate review. |
| Worker suitability | **Candidate only** until display, GPU, presets, determinism, and resource isolation are proven. | Stronger CLI candidate, but remains secondary and requires worker proof. | Candidate for Ender compatibility; current CLI crash reports prevent a production claim. CuraEngine may be easier to isolate but is not yet profile-complete for the target fleet. |

## Bambu Studio assessment

The official [Bambu Studio Command Line Usage](https://github.com/bambulab/BambuStudio/wiki/Command-Line-Usage) documents input models, `--slice`, `--load-settings`, `--load-filaments`, `--outputdir`, `--export-3mf`, `--export-slicedata`, `--info`, and debug/progress options. This confirms an automation surface and profile injection model. It does not by itself prove a no-display server deployment.

The official [Bambu Studio repository](https://github.com/bambulab/BambuStudio) identifies AGPL-3.0 licensing and documents Windows, macOS, and Linux build/release paths. The project’s [headless CLI issue](https://github.com/bambulab/BambuStudio/issues/8293) reports `glfwInit`/Wayland display failures in an Ubuntu AppImage environment, and reports preset/output problems in a Flatpak path. These are observed limitations, not a claim that every release fails identically.

Decision: make Bambu Studio the primary adapter contract, but gate production enablement on a version-pinned proof of concept covering no-display operation (or an approved virtual display), preset fidelity, output extraction, reproducibility, CPU/memory/time limits, and licensing. Do not call it production-ready yet.

## PrusaSlicer assessment

The official [PrusaSlicer CLI documentation](https://github.com/prusa3d/PrusaSlicer/wiki/Command-Line-Interface) confirms command-line slicing and loading profiles from AMF/3MF, with command-line parameters overriding imported settings. The [repository license](https://github.com/prusa3d/PrusaSlicer/blob/master/LICENSE) is AGPL-3.0. This makes PrusaSlicer the preferred secondary/fallback adapter when the compatibility matrix approves the printer, material, process, and profile.

PrusaSlicer is not promoted to primary by this evidence. Linux/container behavior, exact time/material extraction, profile reproducibility, and commercial distribution remain validation and legal work.

## Ender/Creality assessment

The initial tertiary adapter is **Creality Print**, because the official [Creality Print repository](https://github.com/CrealityOfficial/CrealityPrint) documents `--slice`, output directories, printer/process settings, filament settings, logging, Windows/macOS/Linux build paths, and Linux dependencies. The official [CLI crash issue](https://github.com/CrealityOfficial/CrealityPrint/issues/574) reports failures before output on some versions/platforms, including a `calc_exclude_triangles` crash. Therefore Creality Print is a compatibility target and candidate, not a validated production worker.

[CuraEngine](https://github.com/Ultimaker/CuraEngine) is a technically credible future alternative: its official repository describes it as a standalone AGPL-3.0 console application for G-code generation. It is not selected as the default Ender engine yet because printer/material/process definitions and compatibility with the intended Ender fleet need a separate proof. The hierarchy does not assume all Ender models share one profile or one engine.

## OrcaSlicer status

OrcaSlicer is not part of the three official priorities and is not the secondary runtime. It may remain a future comparison/reference because of its relationship to the same slicer ecosystem, but it must not be selected by default or silently substituted during fallback.

## Viewer

Three.js remains the recommended viewer foundation for Stage 05, subject to local packaging and bundle-size review. Its official [documentation](https://threejs.org/docs/) provides loaders, cameras, controls, and renderers; it is not a geometry validator, slicer, or production estimator. The official [license](https://github.com/mrdoob/three.js/blob/dev/LICENSE) is MIT.

No client-side dependency is installed in this stage, and a public CDN is not a runtime requirement.

## Storage

Smartstore’s `IMediaService` and `IMediaStorageProvider` remain the first storage choice because they abstract storage, metadata, transient files, URLs, and provider capabilities. Module tables hold ownership, checksums, artifact roles, processing state, engine provenance, and retention references.

## Compatibility baseline

The host and future module remain aligned to `net11.0`. Existing package versions remain unchanged. Slicer runtimes are external deployment components and must not be smuggled into the Smartstore module package without a separate license, security, platform, and distribution decision.
