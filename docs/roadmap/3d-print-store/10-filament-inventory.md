# Stage 10 — Estoque de filamentos e rastreabilidade

## Objetivo

Controlar estoque em metros e/ou gramas, bobinas, reservas, consumo real e custos de material.

## Atividades

1. Modelar SKU de filamento, material, fabricante, cor, diâmetro, densidade, massa líquida, peso de bobina, lote e fornecedor.
2. Modelar spool individual com peso inicial, tara, saldo estimado, localização, status, lote e histórico.
3. Usar ledger de movimentações imutáveis (entrada, ajuste, reserva, consumo, devolução, perda, transferência); não manter saldo como único registro da verdade.
4. Registrar unidade canônica em massa e comprimento quando disponíveis; conversões dependem de densidade e seção do filamento e devem declarar precisão.
5. Reservar material para job, validar disponibilidade e liberar reserva em cancelamento/falha conforme política.
6. Debitar consumo real ao finalizar ou registrar operação; distinguir previsão do slicer, consumo informado pelo operador e medição por balança/sensor.
7. Permitir múltiplos spools por job, troca de spool, lotes e rastreabilidade.
8. Criar alertas de estoque mínimo, bobina baixa, material vencido/degradado (se aplicável) e divergência de inventário.
9. Integrar custo real ao custo de produção e permitir inventário físico/reconciliação com justificativa.
10. Testar idempotência, concorrência, saldo negativo, conversões, reserva/liberação, devolução e trilha de auditoria.

## Critérios de aceite

- Toda alteração de saldo tem evento, origem, responsável e timestamp.
- Reservas concorrentes não permitem overbooking.
- Previsão e consumo real não são confundidos.
- Consulta em metros e gramas explicita origem e precisão da conversão.