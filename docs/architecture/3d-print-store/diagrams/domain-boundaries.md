# Domain boundaries

```mermaid
flowchart LR
    Assets[ModelAssets] --> Analysis[ModelAnalysis]
    Assets --> Quotes[Quoting]
    Materials[Materials] --> Quotes
    Printers[Printers] --> Quotes
    Quotes --> Commerce[CommerceIntegration]
    Commerce --> Native[Smartstore commerce]
    Commerce --> Production[Production]
    Production --> Inventory[Inventory]
    Materials --> Inventory
    Printers --> Production
```
