# Copilot Instructions for JetLag

## Visão Geral

JetLag é composto por um backend em .NET (C#) e um frontend estático (HTML/CSS/JS). O backend está na raiz do projeto, enquanto o frontend está em `jetlag-frontend/`.

## Estrutura do Projeto

- **Backend (.NET/C#):**
  - Principais arquivos: `Program.cs`, `JetLag.csproj`, `appsettings.json`, `appsettings.Development.json`
  - Configurações de build e execução via `JetLag.sln`.
  - Configurações de ambiente em `Properties/launchSettings.json`.
- **Frontend:**
  - Localizado em `jetlag-frontend/`
  - Estrutura: `public/` (HTML, assets), `src/` (componentes, CSS, JS)

## Fluxos de Desenvolvimento

- **Build Backend:**
  - Use `dotnet build` na raiz para compilar.
  - Executar: `dotnet run` ou execute o binário em `bin/Debug/net8.0/JetLag.dll`.
- **Frontend:**
  - Arquivos estáticos, não requerem build automatizado.
  - Edite diretamente arquivos em `jetlag-frontend/public/` e `jetlag-frontend/src/`.

## Convenções Específicas

- **Configurações:**
  - Use `appsettings.Development.json` para configurações locais.
  - `launchSettings.json` define perfis de execução/debug.
- **Componentização Frontend:**
  - Componentes HTML em `jetlag-frontend/src/components/`.
  - CSS e JS modularizados em subpastas de `src/`.

## Integrações e Dependências

- **Swagger/OpenAPI:**
  - O backend utiliza Swashbuckle para documentação automática da API (veja DLLs Swagger no binário).
- **Comunicação Frontend-Backend:**
  - O frontend consome endpoints do backend via HTTP (veja exemplos em `JetLag.http`).

## Exemplos de Padrões

- **Endpoints:**
  - Definidos em `Program.cs` usando minimal APIs do .NET 8.
- **Frontend Modular:**
  - Exemplo: `src/components/header.html` para cabeçalhos reutilizáveis.

## Recomendações para Agentes

- Priorize modificações nos arquivos de configuração corretos para cada ambiente.
- Siga a estrutura modular do frontend ao criar novos componentes.
- Ao adicionar endpoints, mantenha o padrão minimal API em `Program.cs`.
- Documente endpoints novos usando Swagger.

## Referências

- Backend: `Program.cs`, `JetLag.csproj`, `appsettings*.json`, `JetLag.http`
- Frontend: `jetlag-frontend/public/`, `jetlag-frontend/src/`

---

Adapte instruções conforme novas convenções surgirem. Solicite feedback do usuário para ajustes.
