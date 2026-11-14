# Open decisions

## Product decisions already fixed

1. Runtime priority is Bambu Studio, then PrusaSlicer, then an Ender/Creality-compatible slicing option.
2. Ender is modeled as a printer family/ecosystem, not as a slicing executable.
3. Fallback must be compatibility-based and auditable; no silent change of printer, material, process, nozzle, or profile is permitted.
4. Geometric estimates, slicer-generated estimates, and actual production measurements remain distinct.

## Technical validation still required

1. Can the approved Bambu Studio version run in the target no-display worker, with or without a virtual display, and preserve external printer/process/filament presets?
2. Which Bambu Studio release, AppImage/Flatpak/native packaging, OS image, display stack, and optional components are approved?
3. What exact Bambu artifacts contain reliable time/material values, and how are multi-material results mapped to module materials?
4. Can Bambu Studio produce repeatable results under controlled CPU, memory, and thread settings?
5. Which PrusaSlicer version/profile bundle is the approved secondary worker, and what extraction contract is stable across supported platforms?
6. Which Creality Print version produces stable CLI output for the supported Ender models? Does its current CLI crash behavior block initial production support?
7. Should CuraEngine become the tertiary production path if Creality Print remains unstable, and how will printer/material/process definitions be curated for Ender models?
8. Which future engine alternatives, including OrcaSlicer, are useful as reference only and must remain outside the official priority chain?
9. Which interface set is sufficient for engine discovery, capability declaration, compatibility evaluation, diagnostics, and fallback without prematurely freezing Stage 06 APIs?
10. Do all workers require separate OS/container images, virtual displays, GPU access, or native libraries?

## Legal and distribution questions

11. What legal review is required for AGPL engines, modified workers, server use, binaries, profiles, presets, and source/offering obligations?
12. May Bambu networking components, vendor profiles, Creality Print binaries, or proprietary assets be redistributed, or must operators provision them separately?
13. What trademark and manufacturer-brand wording is permitted for Bambu Lab, Prusa, Creality, and Ender support?

## Product and operational questions

14. What customer roles may upload, and is anonymous upload prohibited?
15. What are maximum upload bytes, decompressed bytes, triangles, dimensions, processing time, and per-customer quotas?
16. Which units and mesh tolerances are accepted when a file is unitless or ambiguous?
17. Is 3MF the preferred customer interchange format, and which OBJ/MTL/AMF extensions are required?
18. Which parser libraries may be redistributed, under what license policy, and who owns vulnerability response?
19. What pricing formula, minimum fee, material waste factor, labor rate, machine rate, tax treatment, and rounding rules apply?
20. How long are drafts, rejected assets, quotes, reports, and artifacts retained? What legal hold and GDPR erasure exceptions apply?
21. What native Smartstore representation will an accepted quote use: dedicated product/variant, hidden product, or another supported pricing path?
22. Which quote states, expiry, deposit, cancellation, rejection, revision, and refund policies apply?
23. What production priority, printer allocation, reprint, failure, and customer notification policies apply?
24. What operator roles can adjust stock, override analysis, approve quotes, or mark actual consumption?
25. Which dashboards and profitability reports are required, and what is the source of truth for cost rates?
26. Which stores/locales/regions and data-residency constraints apply?
