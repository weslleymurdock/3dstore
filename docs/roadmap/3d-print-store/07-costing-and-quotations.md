# Stage 07 — Motor de custos e orçamento

## Objetivo

Calcular uma proposta comercial explicável a partir da análise técnica e das políticas da loja.

## Atividades

1. Definir componentes de custo: material por massa/comprimento, tempo de máquina, energia, setup, mão de obra, acabamento, embalagem, desperdício/falha, taxas, impostos e margem.
2. Usar unidade canônica para cada grandeza. Para converter comprimento e massa, considerar diâmetro e densidade do material; não usar conversão fixa universal.
3. Definir arredondamento, precisão, moeda, impostos e regras de preço com o motor de preços do Smartstore quando possível.
4. Implementar `Quote`, `QuoteLine`, `QuoteCostBreakdown`, validade, versão, status e histórico de revisões.
5. Diferenciar preço calculado, ajuste manual autorizado e preço final aprovado; registrar usuário, motivo e timestamps.
6. Suportar quantidade, variantes (cor/material/acabamento), tolerâncias e serviços adicionais.
7. Aplicar parâmetros de custo configuráveis e snapshots: alteração futura de custo/perfil não deve reescrever orçamento já emitido.
8. Exibir ao admin detalhamento e alertas de baixa confiança; ao cliente exibir proposta clara sem expor dados internos sensíveis.
9. Implementar expiração, reorçamento e controle de concorrência/versão.
10. Testar fórmulas, arredondamentos, conversões, moeda, limites, snapshots e casos extremos.

## Critérios de aceite

- Cada orçamento é reproduzível com snapshots dos parâmetros e análise de origem.
- Ajustes manuais são auditáveis e autorizados.
- Total aprovado é consistente com moeda, impostos e regras de preço do Smartstore.
- Sistema nunca promete tempo/material como valor exato quando são estimativas do slicer.