# Stage 01 — Discovery técnico, requisitos e arquitetura

## Objetivo

Converter a visão de produto em arquitetura aderente à versão real do Smartstore, identificando pontos de extensão, riscos e decisões que precisam ser fechadas antes de criar código de domínio.

## Leituras obrigatórias

- `AGENTS.md`
- `dev-docs/getting-started/architecture-overview.md`
- `dev-docs/getting-started/source-code-organization.md`
- `dev-docs/compose/modules/getting-started-with-modules.md`
- `dev-docs/compose/modules/deploying-modules.md`
- `dev-docs/framework/platform/modularity-and-providers.md`
- `dev-docs/getting-started/data-access.md`
- `dev-docs/getting-started/domain.md`
- `dev-docs/framework/platform/database-migrations.md`
- `dev-docs/framework/platform/security.md`
- `dev-docs/framework/content/media-system-and-imaging.md`
- `dev-docs/compose/modules/localizing-modules.md`

## Atividades

1. Inspecionar os módulos existentes (especialmente DevTools, PayPal, Shipping e OfflinePayment), seus manifests, inicialização, DI, configuração, permissões, migrations e empacotamento.
2. Confirmar TFM/SDK/runtime suportados pelo host. O host atual está em `net11.0`; não definir `net11.0` apenas no plugin. Produzir ADR sobre compatibilidade e plano de migração coordenada, se .NET 11 for requisito incontornável.
3. Mapear pontos de integração com Product, ProductVariant, media, cart, checkout, Order/OrderItem, customer, ACL, taxes, shipping e admin menu. Identificar quais recursos podem ser reutilizados sem customização do core.
4. Definir limites do módulo e namespaces. Nome proposto: `Smartstore.ThreeDPrintStore`; pasta e assembly com o mesmo nome.
5. Definir modelo de venda: produto de catálogo pré-configurado, peça sob encomenda a partir de upload, ou ambos. O fluxo sob orçamento deve ser independente de uma simples adição ao carrinho até existir preço aprovado.
6. Definir formatos iniciais e política de suporte. Avaliar STL, 3MF, OBJ/MTL, AMF e formatos adicionais; especificar unidades, orientação, materiais externos, limites e validação. Não declarar suporte sem parser/preview/slicer compatíveis.
7. Avaliar viewer web (por exemplo Three.js) e slicer/engine pela hierarquia de produto Bambu Studio → PrusaSlicer → solução de slicing para o ecossistema Ender/Creality (inicialmente Creality Print; CuraEngine como alternativa futura), incluindo licenças, execução headless, containers, plataformas, perfis e distribuição. OrcaSlicer não é runtime prioritário. Não assumir que biblioteca de visualização calcula tempo/material.
8. Definir armazenamento de originais, derivados, thumbnails e relatórios; política de retenção, quotas, antivírus e autorização.
9. Modelar os bounded contexts: Catalog/ModelAssets, Quoting, Materials, Printers, Production, Inventory e Integration.
10. Criar ADRs para: compatibilidade runtime; estratégia de armazenamento; engine de slicing; viewer; integração comercial; unidades e conversões; estratégia de migrations.
11. Criar matriz de requisitos funcionais/não funcionais, glossário, diagramas de contexto/fluxo e matriz de riscos.
12. Identificar decisões em aberto que exigem usuário; não inventar políticas comerciais (margem, taxa/hora, tolerância, prazo, depósitos, cancelamentos).

## Fora de escopo

- Não criar entidades, tabelas ou endpoints de produção.
- Não alterar TFM do host, projetos core, pacote NuGet global ou workflow de release.
- Não instalar dependências de viewer/slicer nesta fase.

## Critérios de aceite

- ADRs e diagramas descrevem uma arquitetura viável e modular.
- Toda integração proposta aponta para APIs/extensões reais identificadas no código/documentação.
- Compatibilidade .NET está decidida explicitamente e não deixa um módulo incompatível com o host.
- Formatos, armazenamento, processamento e modelo comercial têm decisões ou perguntas abertas documentadas.
- Roadmap restante atualizado somente se houver descoberta que invalide um stage; registrar motivo.
