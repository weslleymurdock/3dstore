# Stage 11 — Operação, notificações e relatórios

## Objetivo

Completar o fluxo operacional com visão gerencial, comunicação e indicadores úteis.

## Atividades

1. Implementar painéis de pedidos aguardando orçamento, aprovações, fila de impressão, jobs atrasados, impressoras ocupadas/paradas e materiais críticos.
2. Relatórios por período, cliente, produto/modelo, material, cor, impressora, operador e status.
3. Comparar custo estimado versus custo real, tempo estimado versus real, taxa de sucesso, desperdício, retrabalho e margem.
4. Criar alertas configuráveis para orçamento pendente, orçamento expirando, job atrasado, manutenção, falha e estoque mínimo.
5. Usar notificações/eventos/tarefas agendadas existentes do Smartstore; handlers devem ser idempotentes.
6. Exportar CSV/XLSX quando suportado pelas ferramentas existentes e respeitar permissões/ACL.
7. Definir retenção e anonimização de dados operacionais conforme requisitos legais e política da loja.
8. Otimizar consultas, paginação, filtros e índices; evitar carregar grandes volumes em memória.
9. Testar agregações, fuso horário, permissões, exportação, tarefas repetidas e consistência dos indicadores.

## Critérios de aceite

- Indicadores reconciliam com os registros fonte e documentam fórmulas.
- Relatórios respeitam escopo de acesso e não expõem dados de outros clientes.
- Alertas não duplicam em retries e permitem diagnóstico.