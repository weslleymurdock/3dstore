# Stage 09 — Produção e frota de impressoras

## Objetivo

Converter pedidos confirmados em trabalhos executáveis e permitir controle operacional da oficina.

## Atividades

1. Criar `PrintJob`, `PrintJobOperation`, `PrinterAssignment`, `SetupChecklist`, `ProductionEvent` e estados do ciclo de vida.
2. Implementar fila de produção, prioridade, due date, dependências, quantidade e divisão/agrupamento de jobs.
3. Verificar compatibilidade de impressora, volume, bico, material, perfil, disponibilidade e manutenção.
4. Permitir atribuir, reservar, iniciar, pausar, retomar, falhar, cancelar e concluir jobs com motivo e usuário.
5. Registrar setup: limpeza, troca de bico, nivelamento, placa, carga de filamento, temperatura, calibração e checklist customizável.
6. Registrar tempos planejados e reais separadamente; capturar operador, impressora, início/fim, downtime e motivo de falha.
7. Permitir estados de impressora (Available, Reserved, SettingUp, Printing, Paused, Maintenance, Offline, Disabled) e impedir alocações conflitantes.
8. Criar calendário/quadro de produção e detalhe de job para desktop/tablet.
9. Preparar interface opcional para telemetria futura, sem assumir integração automática com firmware ou hardware nesta fase.
10. Testar concorrência de alocação, transições inválidas, falha de impressão, replanejamento e auditoria.

## Critérios de aceite

- Uma impressora não executa dois jobs incompatíveis simultaneamente.
- Setup e tempo de impressão são rastreáveis separadamente.
- Falhas não apagam consumo/tempo já registrados; geram eventos e perdas.
- Operação manual funciona sem conexão direta às impressoras.