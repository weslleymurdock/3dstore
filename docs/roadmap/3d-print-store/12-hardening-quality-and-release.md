# Stage 12 — Hardening, qualidade, empacotamento e release

## Objetivo

Preparar o módulo para instalação e operação real, com segurança, compatibilidade, documentação e evidência de qualidade.

## Atividades

1. Revisar threat model de uploads, parser/slicer, viewer, downloads, admin, APIs, jobs e dados pessoais.
2. Executar testes de autorização/ACL, anti-forgery, ownership, upload malicioso, path traversal, abuso de recursos e concorrência.
3. Validar migrations em banco limpo e upgrade a partir de versões anteriores, em todos os providers oficialmente suportados pelo repositório.
4. Executar build e testes da solução; corrigir warnings introduzidos pelo módulo e regressões.
5. Medir performance de upload, análise, viewer, listagens e relatórios com arquivos de referência.
6. Verificar dependências transitivas, licenças, CVEs, pacotes privados e compatibilidade com runtime do host.
7. Validar instalação, atualização, desativação, reinstalação e desinstalação; preservar dados de negócio conforme política documentada.
8. Documentar instalação, configuração, armazenamento, formatos, engines, perfis, operação, backup/restore e troubleshooting.
9. Criar fixtures de demonstração não comerciais e exemplos pequenos para QA, sem incluir arquivos de terceiros sem licença.
10. Preparar artefato de módulo e instruções de implantação conforme o packager/deploy do Smartstore.
11. Executar smoke test ponta a ponta: upload → análise → orçamento → aprovação → pedido → job → impressão simulada → consumo → baixa de estoque → relatório.

## Critérios de aceite

- Build, testes, migrations e smoke tests passam com logs anexados.
- Nenhuma vulnerabilidade crítica/alta conhecida introduzida permanece sem mitigação/aceite explícito.
- Instalação e upgrade são reproduzíveis e documentados.
- Dependências, licenças, formatos e engines suportados estão documentados.
- Módulo pode ser distribuído sem arquivos locais/segredos e sem editar diretórios de build output.
- Relatório final lista limitações reais e itens futuros (telemetria, integração direta com impressoras, automação de manutenção, múltiplas filiais, etc.).