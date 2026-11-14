# Stage 04 — Upload, armazenamento e pipeline de arquivos 3D

## Objetivo

Permitir que o cliente envie modelos 3D com segurança e que o sistema gerencie originais e artefatos derivados.

## Atividades

1. Implementar upload autenticado/guest conforme política definida, associado ao customer e a uma intenção de orçamento.
2. Integrar com o sistema de mídia/storage provider do Smartstore por abstração; manter binários fora do banco relacional.
3. Validar extensão permitida, MIME como sinal auxiliar, assinatura/conteúdo real, tamanho, quantidade de arquivos, nomes, checksums e quotas.
4. Armazenar em estado de quarentena até validação; gerar identificadores opacos e nunca confiar em path enviado pelo cliente.
5. Implementar estados de processamento (Received, Validating, Ready, Rejected, Failed, Archived) e histórico de transições.
6. Definir adapters por formato e versão; preservar dependências como MTL/texturas quando permitidas e validar arquivos referenciados.
7. Proteger endpoints de download/preview por autorização, ACL, ownership e expiração de links quando aplicável.
8. Implementar limites contra ZIP bombs, decompression bombs, arquivos excessivos, traversal e payloads malformados.
9. Criar limpeza/retention configurável e tarefas assíncronas idempotentes para processamento e remoção de temporários.
10. Registrar auditoria, checksum, tamanho, formato detectado, dimensões/unidades quando conhecidas e origem.
11. Criar testes de arquivos válidos/inválidos, payloads hostis, autorização, concorrência e falhas de storage.

## Critérios de aceite

- Upload não disponibiliza arquivo antes da validação.
- Nenhum caminho físico ou chave interna é exposto ao usuário.
- Falha de processamento não deixa upload em estado ambíguo.
- Arquivos originais permanecem imutáveis; derivados são versionados e rastreáveis.
- Limites e mensagens de erro são configuráveis/localizados.