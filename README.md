# OWASP Juice Shop — Playwright UI + API Test Framework (C#/.NET)

Portfolio project showcasing an **SDET-friendly test framework on top of Playwright for .NET** for the OWASP Juice Shop web app.

## Playwright TypeScript version

Also available as a Playwright TypeScript version: https://github.com/notNullThen/owasp-js-playwright-api-typescript

## Project overview

This repository contains a test automation framework for OWASP Juice Shop.

It demonstrates a practical automation architecture with both UI and API flows along with an API tooling layer which significantly simplifies API waiting/interaction.

- **UI tests**: validate key user workflows (e.g., registration, login, search).
- **API tooling**: used for fast setup/verification and for asserting UI-triggered network calls.

**Note on test coverage:**
This suite demonstrates framework capabilities and a core testing strategy. More extensive coverage (negative cases, additional flows, etc.) can be added on top of the same architecture.

### Reporting: Allure Report 3 (Allure CLI)

Allure is used via the Allure CLI. Test results are written to:

- `OwaspPlaywrightTests/bin/Debug/net10.0/allure-results`

In Docker, Allure is installed and the report is served automatically.

### Traces

Playwright tracing is enabled for each test (see `OwaspPlaywrightTests/Base/PlaywrightTestBase.cs`).

- Traces are saved under `OwaspPlaywrightTests/playwright-traces` as `.zip`
- Each test trace is also attached to Allure

## Benefits / what this demonstrates

- **Unified UI + API approach**: UI flows stay readable while setup stays fast.
- **Network-aware UI assertions**: the same endpoint definitions support both API `RequestAsync()` and UI `WaitAsync()`.
- **Parallel-friendly design**: `AsyncLocal` API clients + per-test Playwright context to avoid cross-test leaks.
- **Docker-ready solution**: Juice Shop + tests + Allure can run end-to-end via Docker Compose.

## Prerequisites

- .NET SDK (this project targets **.NET 10**)
- Optional: Node.js (only needed if you want to run the Allure CLI locally)
- Optional: Docker + Docker Compose (recommended for a reproducible and stable demo)

## Running tests

### Docker (recommended)

The project is configured to run tests in Docker using the official Playwright .NET Docker image.

**To run tests in Docker:**

1. Clone the repository and navigate to it:

```bash
git clone https://github.com/notNullThen/owasp-js-playwright-api-csharp.git
cd owasp-js-playwright-api-csharp
```

2. Start Juice Shop + run tests + serve Allure:

```bash
docker compose up playwright --build
```

This will:

- Start OWASP Juice Shop on `http://localhost:3000`
- Run the test suite and serve the Allure report.

### GitHub Codespaces

1. Wait till all the commands in Codespace are executed (as a result, the `.env` file should appear)

1. Run all tests:

```bash
dotnet test
```

3. Open Allure reports:
```bash
allure serve OwaspPlaywrightTests/bin/Debug/net10.0/allure-results
```

### Local (run tests on your machine)

1. Start OWASP Juice Shop:

```bash
docker run --rm -p 3000:3000 bkimminich/juice-shop:latest
```

2. Clone the repository and navigate to it:

```bash
git clone https://github.com/notNullThen/owasp-js-playwright-api-csharp.git
cd owasp-js-playwright-api-csharp
```

3. Create `.env` file from `.env.example`:

```bash
cp .env.example .env
```

4. Run all tests:

```bash
dotnet test OwaspPlaywrightTests
```

5. (Optional) Open Allure report locally (requires Allure CLI):

```bash
npm i -g allure
allure serve OwaspPlaywrightTests/bin/Debug/net10.0/allure-results --port 8080
```

## Configuration

Environment variables are loaded from `.env` via `DotNetEnv` (see `OwaspPlaywrightTests/TestConfig.cs`).

❗️ For demonstration and convenience purposes the `.env.example` already contains values.

### Base URL selection

Base URL switches automatically in Docker/CI:

- Locally (no `CI` env var): `http://localhost:3000`
- In Compose/CI (`CI=true`): `http://juice-shop:3000`

## Running options

- Run all tests:

```bash
dotnet test OwaspPlaywrightTests
```

- Run a single test class (example):

```bash
dotnet test OwaspPlaywrightTests --filter "FullyQualifiedName~OwaspPlaywrightTests.Tests.UI.UsersTest"
```

## API tooling

The API tooling layer is located under `OwaspPlaywrightTests/Base/ApiHandler/`. It provides a fluent interface for defining endpoints once and reusing them across both API and UI tests.

### Architecture

- `OwaspPlaywrightTests/Base/ApiHandler/ApiAction.cs`: exposes `RequestAsync()` and `WaitAsync()` depending on context
- `OwaspPlaywrightTests/Base/ApiHandler/ApiBase.cs`: executes requests / waits for responses, parses JSON, validates status codes
- `OwaspPlaywrightTests/Base/ApiHandler/ApiParametersBase.cs`: builds normalized URLs, stores base URL / token, and applies per-call request parameters

### Usage examples

**Direct API request (fast setup):**

```csharp
using OwaspPlaywrightTests.ApiEndpoints;

var response = await Api.RestUser.PostLogin(new() { Email = email, Password = password }).RequestAsync();
```

**Wait for UI-triggered API call:**

```csharp
// Example pattern inside a Page Object method
var loginResponseTask = Api.RestUser.PostLogin().WaitAsync();
await Task.WhenAll(LoginButton.ClickAsync(), loginResponseTask);
var loginResponse = await loginResponseTask;
```

## Auth & parallel execution

- API auth is applied via a bearer token set on the API tooling (`ApiParametersBase.SetToken(...)`).
- Common flows are provided as hooks under `OwaspPlaywrightTests/Hooks/` (e.g. `CreatedUserHook`, `AuthenticatedHook`, `AuthenticatedUiHook`).
- API endpoint wrappers are stored per-async-flow via `AsyncLocal` (see `OwaspPlaywrightTests/ApiEndpoints/Api.cs`).

## Example tests

- API auth flow: `OwaspPlaywrightTests/Tests/API/AuthenticationTest.cs`
- UI auth flows: `OwaspPlaywrightTests/Tests/UI/UsersTest.cs`
- Search flow: `OwaspPlaywrightTests/Tests/UI/SearchTest.cs`

## Project structure

- `OwaspPlaywrightTests/ApiEndpoints/` — API client wrappers + endpoint definitions
- `OwaspPlaywrightTests/Base/ApiHandler/` — API tooling (request + wait abstraction)
- `OwaspPlaywrightTests/Pages/` — Page Objects
- `OwaspPlaywrightTests/Components/` — reusable UI Components
- `OwaspPlaywrightTests/Tests/` — test specs (API/UI/System)
- `OwaspPlaywrightTests/Data/` — test data builders
- `OwaspPlaywrightTests/Support/` — utilities and helpers

## Notes

- API calls validate expected status codes by default 200/201 (see `OwaspPlaywrightTests/TestConfig.cs`).
- API failures include endpoint/method context and HTTP status.
