# ADR-010: Security and isolation of file processing

## Context

Uploaded 3D packages are attacker-controlled input. Parsers and slicers can consume substantial resources and may invoke native code.

## Problem

The platform must protect the web process, other customers, model confidentiality, and quote integrity.

## Alternatives

1. Parse/slice in the web process.
2. Execute user-provided tools or G-code.
3. Quarantine, validate, and process with allowlisted code in a least-privilege isolated worker.

## Decision

Choose option 3. Enforce content sniffing, archive/XML protections, quotas, timeouts, memory/CPU limits, no unnecessary network, opaque paths, authorization on every artifact, immutable checksums, audit records, and safe cleanup. Only deployment-owned engines may run.

## Consequences

Processing becomes an asynchronous operational subsystem with deployment and monitoring requirements. Security evidence is part of the analysis result.

## Risks

Isolation strength differs by container/host platform. Release validation must include escape, quota, path, SSRF, malware-scan, and authorization tests.

## Open questions

Which worker runtime, sandbox technology, malware scanner, and deployment network policy will be approved?
