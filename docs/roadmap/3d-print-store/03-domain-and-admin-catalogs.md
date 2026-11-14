# Stage 03 — Domínio e cadastros administrativos

## Objetivo

Implementar os conceitos de negócio fundamentais e suas telas administrativas, sem ainda processar uploads nem iniciar produção.

## Modelo inicial esperado

- **PrintingMaterial:** família/material (PLA, PETG, ABS, ASA, TPU, resinas se aplicável), fabricante, propriedades físicas, temperatura e compatibilidade.
- **FilamentProfile:** diâmetro nominal, densidade, cor, tolerâncias, temperatura, custo de referência e unidade comercial.
- **Printer:** fabricante/modelo, tecnologia, volume útil X/Y/Z, bicos, capacidades, estado operacional e localização.
- **PrinterProfile:** parâmetros de impressão e associação compatível entre impressora, material e bico.
- **PrintServiceConfiguration:** custos e políticas configuráveis (energia, mão de obra, overhead, margem, setup, falha/desperdício).
- **ModelAsset** e **Quote** devem ter apenas contratos/relacionamentos preliminares se necessários; comportamento completo fica nos stages próprios.

## Atividades

1. Refinar invariantes, cardinalidades, enums e value objects com base nos ADRs.
2. Implementar entidades, configurações e validações de domínio; não colocar regras de negócio em controllers ou views.
3. Criar FluentMigrator migrations, FKs, índices e restrições para entidades deste stage.
4. Implementar serviços de aplicação e repositórios usando abstrações Smartstore existentes.
5. Criar CRUD admin para materiais, filamentos, impressoras, perfis e parâmetros operacionais.
6. Validar combinações incompatíveis (por exemplo, material sem perfil suportado pela impressora) sem codificar listas fixas no frontend.
7. Implementar permissões separadas para visualizar, criar, editar, desativar e administrar configurações.
8. Localizar labels, validações, mensagens e tooltips EN/DE; propriedades de modelos admin devem usar `LocalizedDisplay` e recursos Fields/Fields.*.Hint.
9. Testar invariantes, conversões/unidades básicas, persistência, permissões e operações CRUD.

## Critérios de aceite

- Cadastros persistem e são administráveis por usuários autorizados.
- Exclusão não destrói registros referenciados; usar desativação/soft delete quando adequado.
- Restrições e relações são protegidas no domínio e no banco.
- Nenhuma regra operacional crítica depende de valores hardcoded na UI.