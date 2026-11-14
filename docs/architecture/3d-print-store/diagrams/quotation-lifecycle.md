# Quotation lifecycle

```mermaid
stateDiagram-v2
    [*] --> Draft
    Draft --> Submitted
    Submitted --> UnderReview
    UnderReview --> Offered
    UnderReview --> Rejected
    Offered --> Accepted
    Offered --> Rejected
    Offered --> Expired
    Accepted --> CommerceLinkPending
    CommerceLinkPending --> CartReady
    CartReady --> Ordered
    CommerceLinkPending --> ReconciliationRequired
```
