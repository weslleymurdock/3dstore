# Stage 02 — Esqueleto do módulo e persistência

## Objetivo

Criar o módulo instalável, carregável e configurável, com estrutura consistente e base técnica para os próximos stages.

## Atividades

1. Criar `src/Smartstore.Modules/Smartstore.ThreeDPrintStore/` com projeto Razor SDK, target framework compatível com o host, output path em `src/Smartstore.Web/Modules/Smartstore.ThreeDPrintStore`, referências herdadas por `Smartstore.Module.props` e referência a Web apenas se necessária.
2. Criar `module.json` válido conforme `module.schema.json`, `Module.cs`, `Startup.cs`, `Localization/resources.en-us.xml`, `Localization/resources.de-de.xml`, `Views/_ViewImports.cshtml` e pastas iniciais de configuração, domínio, migrations, serviços e views.
3. Seguir padrão ModuleBase para instalação/desinstalação, importação/remoção de recursos e settings. Implementar IConfigurable e rota de configuração inicial.
4. Criar registro DI usando StarterBase; não usar service locator, não registrar serviços desnecessários e respeitar lifetimes.
5. Integrar assembly do módulo à infraestrutura de persistência por `IDbContextConfigurationSource<SmartDbContext>`/DbContext factory e `AddModelAssembly`, após confirmar o padrão aplicável nesta versão.
6. Configurar migrations FluentMigrator do módulo seguindo o padrão do repositório. Garantir descoberta/ordenação e estratégia de upgrade; não usar EF migrations.
7. Criar permission provider e permissões-raiz para acesso admin; definir política de autorização por ação.
8. Criar menu admin protegido por permissão e tela de configuração mínima, com recursos localizados.
9. Adicionar projeto ao `Smartstore.sln` apenas se os projetos de módulos não forem incluídos automaticamente; preservar estrutura da solução e build.
10. Adicionar testes para manifest/descoberta, instalação, configuração DI, permissões e migration bootstrap conforme infraestrutura existente.

## Critérios de aceite

- Projeto compila no TFM do host e aparece no build sem cópia manual.
- Módulo é descoberto, instalável e desinstalável sem exceções.
- Instalação aplica schema inicial e desinstalação segue política de preservação de dados documentada.
- Admin menu e configuração exigem permissão correta.
- Recursos EN/DE importam corretamente.
- Build da solução e testes relevantes passam.
- Nenhum arquivo é editado em `src/Smartstore.Web/Modules`.