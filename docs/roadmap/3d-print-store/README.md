# Smartstore 3D Print Store — Roadmap

## Objetivo

Evoluir o Smartstore em uma loja de impressão 3D por meio de um módulo instalável, localizado em `src/Smartstore.Modules/Smartstore.ThreeDPrintStore`. O módulo deve permitir que clientes enviem modelos 3D, visualizem os arquivos, solicitem orçamentos e comprem peças impressas; e que a operação administre materiais, impressoras, produção, estoque de filamento, custos e prazos.

Este roadmap orienta agentes locais (Codex, Antigravity e Copilot). Cada stage deve ser executado isoladamente, revisado e validado antes do próximo.

## Estado inicial verificado

- Repositório: `weslleymurdock/Smartstore`; branch de trabalho: `feat/3d-store`.
- A branch `feat/3d-store` existia e apontava para o mesmo commit de `main` no momento da criação deste roadmap.
- Smartstore está na versão `6.4.0`; `src/Smartstore.Build/Smartstore.Common.props` define `TargetFramework=net11.0`.
- Os módulos são bibliotecas Razor em `src/Smartstore.Modules/<Name>`, usam `module.json`, `Module.cs` e `Startup.cs`, e são carregados de `src/Smartstore.Web/Modules` (saída de build, nunca editar).
- O repositório determina FluentMigrator para alterações de schema; não usar EF Core migrations.
- `AGENTS.md` e `dev-docs/` são fontes normativas. Consultar a documentação específica antes de implementar cada subsistema.

## Regra de compatibilidade .NET

O pedido de produto menciona .NET 11, mas o host verificado nesta branch compila em `net11.0`. Um módulo carregado no processo do Smartstore precisa ser compatível com o runtime e com as assemblies do host. Portanto:
1. Não criar um módulo `net11.0` isolado enquanto o host permanecer em `net11.0`.
2. No Stage 01, verificar a versão oficial e o suporte do SDK/runtime disponível, o target framework do host e a política de compatibilidade do Smartstore.
3. Implementar o módulo no mesmo TFM do host atual (`net11.0`) para manter integração executável; planejar eventual migração coordenada do host e módulo para .NET 11 como trabalho separado, caso o usuário confirme que o fork deve migrar integralmente.
4. Não alterar o TFM global, SDK, dependências centrais ou projetos do host como efeito colateral do plugin.

## Princípios obrigatórios

- **Module-first:** funcionalidades específicas de impressão 3D pertencem ao módulo. Não fazer fork de funcionalidades core nem alterar `src/Smartstore.Core` ou `src/Smartstore.Web` sem justificativa técnica demonstrada e autorização explícita.
- **Reutilizar Smartstore:** catálogo, clientes, ACL/permissões, mídia, carrinho, checkout, pedidos, impostos, frete, preços, tarefas, notificações e localization existentes sempre que adequados.
- **Domínio explícito:** orçamento, modelo 3D, material, perfil de impressão, impressora, trabalho de produção, spool e movimentação de estoque são conceitos distintos; não reduzir tudo a campos de produto ou GenericAttribute.
- **Persistência:** FluentMigrator; migrations versionadas, idempotentes/defensivas conforme padrões locais, índices, FKs e reversão quando aplicável. Usar o DbContext factory/model assembly do Smartstore para mapear entidades do módulo.
- **Arquivos:** não gravar binários 3D no banco. Usar abstração de armazenamento integrada ao sistema de mídia/armazenamento do Smartstore; validar autorização, tamanho, extensão, conteúdo real, checksum e estado de processamento.
- **Cálculo confiável:** estimativas preliminares nunca devem ser confundidas com resultado de fatiador. Preferir dados de slicer real e registrar engine, versão, perfil e parâmetros usados.
- **Segurança:** tratar arquivos enviados como conteúdo não confiável; isolamento de processamento, limites de CPU/memória/tempo, proteção contra path traversal, malware e arquivos compactados maliciosos.
- **Operação auditável:** estados e transições explícitos, histórico imutável de consumo/movimentação e trilha de auditoria para decisões e ajustes.
- **UX:** storefront responsivo e acessível; administração adequada a operação de oficina; localização em inglês e alemão seguindo o padrão do repositório.
- **Dependências:** não introduzir biblioteca pesada, serviço externo obrigatório, CDN runtime ou dependência nativa sem avaliar licença, segurança, manutenção, implantação e alternativa.
- **Testes reais:** testes de domínio, integração de persistência, endpoints e fluxos. Não criar testes artificiais apenas para cobertura.

## Sequência

| Stage | Entrega |
|---|---|
| 01 | Discovery técnico, requisitos, ADRs e arquitetura executável |
| 02 | Esqueleto do módulo, integração e infraestrutura de persistência |
| 03 | Domínio e cadastros administrativos |
| 04 | Upload, armazenamento e pipeline de arquivos 3D |
| 05 | Visualizador 3D no storefront e administração |
| 06 | Análise geométrica e integração com slicer |
| 07 | Motor de custos, estimativas e orçamento |
| 08 | Aprovação comercial e integração com carrinho/pedido |
| 09 | Planejamento de produção e gestão de impressoras |
| 10 | Estoque de filamentos e rastreabilidade de consumo |
| 11 | Operação, notificações, relatórios e indicadores |
| 12 | Segurança, qualidade, empacotamento e release |

## Protocolo de execução por stage

1. Ler `AGENTS.md`, este README e o stage solicitado.
2. Ler os documentos `dev-docs/` indicados no stage e inspecionar implementações existentes antes de propor abstrações.
3. Registrar decisões e limitações descobertas. Não presumir que o roadmap autoriza mudanças no core.
4. Implementar somente o escopo do stage, incluindo testes e localização necessárias.
5. Executar os comandos de build/test indicados e reportar resultados reais, sem declarar validação que não ocorreu.
6. Não criar commits, trocar de branch, fazer push ou abrir PR sem solicitação explícita do usuário.
7. Ao concluir, entregar resumo, arquivos alterados, testes executados, pendências e confirmação objetiva do critério de aceite. Não iniciar o próximo stage automaticamente.

## Critério global de pronto

O módulo pode ser instalado/desinstalado sem quebrar a loja; cliente pode enviar um arquivo suportado, visualizar, receber orçamento revisável, aprová-lo e concluir pedido; equipe pode converter pedidos em jobs, alocar impressoras, registrar setup/execução, consumir filamento e reconciliar estoque. Valores, arquivos, decisões, status e eventos críticos devem ser rastreáveis.