# Production lifecycle

```mermaid
stateDiagram-v2
    [*] --> Planned
    Planned --> Queued
    Queued --> Allocated
    Allocated --> Setup
    Setup --> Printing
    Printing --> Paused
    Paused --> Printing
    Printing --> Failed
    Failed --> Queued: rework/reprint approved
    Printing --> Completed
    Completed --> Verified
    Verified --> [*]
```
