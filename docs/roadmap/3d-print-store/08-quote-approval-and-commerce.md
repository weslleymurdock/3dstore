# Stage 08 — Aprovação comercial e integração com carrinho/pedido

## Objetivo

Conectar orçamento aprovado ao fluxo nativo de compra do Smartstore sem quebrar checkout, impostos, pagamentos ou pedidos.

## Atividades

1. Implementar portal do cliente para acompanhar status, solicitar revisão, aceitar ou rejeitar orçamento e consultar validade.
2. Criar workflow explícito (Draft, Submitted, UnderReview, NeedsChanges, Approved, Rejected, Expired, Accepted, Converted, Cancelled) com transições validadas.
3. Notificar cliente e equipe em mudanças relevantes usando infraestrutura existente.
4. Somente orçamento aprovado e aceito pode gerar item comprável; proteger contra alteração de preço entre aceite e checkout.
5. Integrar ao carrinho/pedido com extensão suportada (custom line item data, product attributes ou mecanismo identificado no Stage 01). Evitar duplicar Order/Checkout.
6. Preservar vínculo quote/model asset/análise/versão no pedido e garantir que o snapshot comercial não mude após compra.
7. Definir depósito/pagamento integral, política de cancelamento, reembolso e prazo somente após decisão comercial do usuário.
8. Lidar com múltiplas quantidades, reordenação, expiração, estoque e indisponibilidade de impressora.
9. Proteger ownership, permissões de admin e endpoints anti-forgery.
10. Testar o fluxo completo de orçamento a pedido, preço adulterado, duplicidade, concorrência e callbacks de pagamento.

## Critérios de aceite

- Não é possível comprar orçamento rejeitado, expirado ou não aceito.
- Pedido mantém snapshot e referências de produção necessárias.
- Checkout nativo continua funcionando para produtos não relacionados à impressão 3D.
- Nenhum preço enviado pelo browser é considerado autoridade.