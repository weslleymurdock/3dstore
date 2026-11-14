# 3D file format evaluation

## Support policy

Stage 01 recommends a capability matrix rather than a blanket “supported” label. A format is supported only when the pipeline has a parser, validation rules, viewer conversion, and slicer-compatible hand-off tested against representative files.

| Format | Initial posture | Geometry/units | Materials/textures | Main risks and next proof |
| --- | --- | --- | --- | --- |
| STL | Phase 1 candidate | Triangle mesh; commonly unitless, frequently interpreted as mm | No reliable material/texture model | Detect binary vs ASCII, validate triangle count/normals/manifoldness, require unit policy |
| 3MF | Preferred interchange candidate | Package can carry units, multiple objects, transforms and metadata | Richer package model; textures/materials depend on extensions | ZIP/package security, relationships, extension support, validate against 3MF Core specification |
| OBJ + MTL | Phase 1 candidate with paired-file rules | Mesh; unit convention is external | MTL may reference materials/textures | Require safe package handling and complete relative-reference resolution; no orphan external files |
| AMF | Deferred candidate | XML mesh with units and multiple objects | Color/material support exists in the format | XML limits/XXE defenses, parser maturity, slicer interoperability |
| G-code | Output only | Machine instructions, not an accepted customer input | Machine/material specific | Never execute or treat as trusted input; expose only generated output from an isolated, pinned engine |

## Normalized internal representation

Adapters should produce a bounded normalized representation for validation/viewing/analysis: vertices/faces or a streamable mesh reference, object identity, transforms, declared unit, bounding box, material references, and warnings. The original file remains immutable. Conversion must be deterministic and record adapter/version/checksum.

## File limits

The actual limits are open policy decisions and must be configuration-backed, not constants hidden in constructors. They must cover upload bytes, decompressed package bytes, number of archive entries, triangles/objects, recursion, dimensions, processing time, memory, derivative count, and per-customer quota. ZIP bombs, path traversal, symlink-like entries, external references, entity expansion, and malformed numeric values are rejection cases.

## Licensing and interoperability

STL/OBJ/AMF are formats/specifications, not a reason by themselves to add a parser package. 3MF has an official specification and extension ecosystem. Any implementation library must be reviewed for license, maintenance, native dependencies, security advisories, and redistribution. No library is selected or installed in Stage 01.
