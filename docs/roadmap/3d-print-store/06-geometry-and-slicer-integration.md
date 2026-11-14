# Stage 06 — Análise geométrica e integração com slicer

## Objetivo

Transformar um arquivo validado em dados técnicos confiáveis para orçamento e planejamento.

## Atividades

1. Definir contrato `IModelAnalysisService` e adapters por formato/engine; manter domínio independente de slicer específico.
2. Extrair dimensões, volume, área, número de corpos, bounding box, unidades, malha aberta/não-manifold e orientações relevantes.
3. Definir estratégia de reparo: somente diagnóstico ou reparo automático com consentimento e preservação do original; armazenar derivado separado.
4. Integrar com slicer real em processo isolado (por exemplo, PrusaSlicer/OrcaSlicer ou alternativa aprovada), executável/container configurável e versionado.
5. Aplicar perfil compatível de impressora/material, bico, layer height, infill, supports, walls, brim/raft, temperatura e demais parâmetros.
6. Capturar output estruturado do slicer: massa/filamento, comprimento quando fornecido, tempo estimado, suportes, custo/consumo de energia quando disponível e avisos.
7. Definir timeout, cancelamento, limites CPU/memória/disco, concorrência e isolamento; nunca executar G-code gerado ou binário enviado sem aprovação operacional.
8. Persistir cada execução como análise versionada com engine, versão, hash do arquivo, perfil, parâmetros, logs sanitizados e resultado.
9. Tratar falhas, incompatibilidade de perfil e resultados parciais como estados explícitos.
10. Criar testes com fixtures legais e pequenos, resultados determinísticos simulados no adapter e testes de integração opcionais com slicer instalado.

## Critérios de aceite

- Estimativa apresentada sempre referencia a análise e o perfil que a produziram.
- Repetir a análise com arquivo/perfil/versão diferentes cria nova versão, sem sobrescrever histórico.
- O sistema diferencia dados medidos pelo slicer de heurísticas aproximadas.
- Processamento não bloqueia requisição web nem executa conteúdo não confiável no processo principal.