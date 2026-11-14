# ADR-004: 3D file storage architecture

## Context

Smartstore exposes `IMediaService` and `IMediaStorageProvider`, including transient files, metadata, URLs, streaming, and provider capability information.

## Problem

Original models and derivatives may be large, private, versioned, and subject to asynchronous cleanup.

## Alternatives

1. Store blobs in module database columns.
2. Write directly to a module filesystem path.
3. Use Smartstore media/storage abstractions and module metadata tables.

## Decision

Store original and derivative blobs through Smartstore media/storage abstractions. Module records store ownership, role, checksum, size, source version, processing status, authorization and retention references. Quarantine and opaque names are mandatory.

## Consequences

The module inherits provider portability and media lifecycle conventions. Large-file streaming and worker access must be verified for each deployment provider.

## Risks

Public media URL behavior may be too permissive for private models; every original/report/download route needs an explicit authorization gate.

## Open questions

Whether the deployment’s media provider supports the required quota, streaming, backup, and separate worker access.
