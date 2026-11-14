# Stage 05 — Visualizador 3D

## Objetivo

Exibir os modelos no storefront e no admin, com controles intuitivos e limites seguros.

## Atividades

1. Escolher biblioteca viewer no Stage 01 e validar licença, tamanho, manutenção e compatibilidade com pipeline de assets Smartstore. Não carregar biblioteca por CDN pública em runtime.
2. Implementar viewer encapsulado no módulo, com carregamento sob demanda e fallback para thumbnail/download quando WebGL não estiver disponível.
3. Renderizar somente artefatos aprovados pelo pipeline; não abrir URL arbitrária ou recursos externos referenciados pelo arquivo.
4. Implementar câmera orbit, zoom, pan, reset, enquadramento automático, grade/eixos opcionais e indicação de unidade/dimensões quando confiáveis.
5. Suportar apenas formatos cujos loaders foram validados. Resolver materiais/texturas com allowlist e caminhos confinados.
6. Criar geração de thumbnail server-side ou worker isolado quando viável; manter alternativa client-side segura.
7. Integrar viewer a produto, detalhe de orçamento e tela de revisão admin.
8. Considerar acessibilidade: controles por teclado, rótulos, estados de carregamento e alternativa textual.
9. Medir tamanho dos assets, tempo de render, uso de memória e comportamento em mobile.
10. Testar modelos grandes, inválidos, sem material, com múltiplos objetos e diferentes escalas.

## Critérios de aceite

- Viewer não executa scripts nem faz requisições externas originadas do modelo.
- Usuário consegue inspecionar geometria sem alterar o arquivo original.
- Falhas WebGL/loader exibem fallback funcional.
- Assets estão integrados ao pipeline do módulo e não aumentam desnecessariamente o bundle global.