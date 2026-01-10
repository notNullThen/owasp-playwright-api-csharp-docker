# OWASP Juice Shop — Playwright UI + API Test Framework (C#/.NET)

Portfolio project demonstrating an **SDET-friendly** automation framework for the OWASP Juice Shop app: **Playwright UI + API testing**, a reusable **API tooling layer** (request + wait), **Allure 3 reporting**, and **Dockerized execution**.

## Playwright TypeScript/NodeJS version

Also available as a Playwright TypeScript NodeJS version: https://github.com/notNullThen/owasp-playwright-api-typescript-docker

## Framework Features

- **Test architecture**: Page Objects + reusable UI Components + data builders + hooks.
- **UI + API combined**: API calls for fast setup/verification; UI flows stay readable.
- **Network-aware assertions**: the same endpoint definitions support API `RequestAsync()` and UI `WaitAsync()`.
- **Parallel-safe design**: per-test Playwright context + `AsyncLocal` API clients to avoid cross-test leaks.
- **Engineering hygiene**: configuration via `.env`, CI/Docker-friendly base URL switching, formatted with CSharpier & configured `.editorconfig` file, structured test reporting.

## Quick start

Runs Juice Shop, executes the full suite (API + UI), and serves Allure:

### Docker (recommended)

The project is configured to run tests in Docker using the official Playwright .NET Docker image.

**To run tests in Docker:**

1. Clone the repository and navigate to it:

```bash
git clone https://github.com/notNullThen/owasp-playwright-api-csharp-docker.git
cd owasp-playwright-api-csharp-docker
```

2. Start Juice Shop + run tests + serve Allure:

```bash
docker compose up playwright --build
```

### Run in GitHub Codespaces

1. Wait until all the Codespace init commands complete (the `.env` file should appear)

2. Run all tests:

```bash
dotnet build -t:RunAllTests /tl:false
```

3. Open Allure report:
```bash
allure serve OwaspPlaywrightTests/bin/Debug/net10.0/allure-results
```


## Run locally

1. Start Juice Shop:

```bash
docker run --rm -p 3000:3000 bkimminich/juice-shop:latest
```

2. Create env file and run tests:

```bash
cp .env.example .env
dotnet build -t:RunAllTests /tl:false
```

If Playwright dependencies aren’t installed yet:

```bash
OwaspPlaywrightTests/bin/Debug/net10.0/playwright.sh install
```

Allure locally (optional):

```bash
npm i -g allure
allure serve OwaspPlaywrightTests/bin/Debug/net10.0/allure-results --port 8080
```

## Suites / filtering

This repo uses xUnit Traits:

- API only: `dotnet build -t:RunAPITests /tl:false` (or `dotnet test OwaspPlaywrightTests --filter "Suite=API"`)
- UI only: `dotnet build -t:RunUITests /tl:false` (or `dotnet test OwaspPlaywrightTests --filter "Suite=UI"`)

## Configuration

- Environment variables load from `.env` via `DotNetEnv` (see `OwaspPlaywrightTests/TestConfig.cs`).
- Base URL switches automatically:
	- Local: `http://localhost:3000`
	- Docker/CI (`CI=true`): `http://juice-shop:3000`

## API tooling (the interesting bit)

API layer lives in `OwaspPlaywrightTests/Base/ApiHandler/` and is designed so endpoints are defined once and reused for:

- direct API setup/verification via `RequestAsync()`
- UI-network assertions via `WaitAsync()`

Example (wait for UI-triggered API call):

```csharp
var loginResponseTask = Api.RestUser.PostLogin().WaitAsync();
await Task.WhenAll(LoginButton.ClickAsync(), loginResponseTask);
var loginResponse = await loginResponseTask;
```

## Where to look

- Tests: `OwaspPlaywrightTests/Tests/API/`, `OwaspPlaywrightTests/Tests/UI/`
- API endpoints: `OwaspPlaywrightTests/ApiEndpoints/`
- API handler: `OwaspPlaywrightTests/Base/ApiHandler/`
- Pages/components: `OwaspPlaywrightTests/Pages/`, `OwaspPlaywrightTests/Components/`
- Hooks: `OwaspPlaywrightTests/Hooks/`
