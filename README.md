# OWASP Juice Shop — Advanced Test Framework

[![Playwright](https://img.shields.io/badge/Playwright-C%23-2E8B57?logo=playwright)](https://playwright.dev/dotnet/)
[![Docker](https://img.shields.io/badge/Docker-Ready-2496ED?logo=docker)](https://www.docker.com/)
[![Allure](https://img.shields.io/badge/Reporting-Allure-FF6C37?logo=allure)](https://allurereport.org/)
[![NuGet](https://img.shields.io/badge/Package-SimpleApiPlaywright-blue)](https://www.nuget.org/packages/SimpleApiPlaywright/)

A professional-grade SDET project demonstrating a modern, containerized automation framework for the **OWASP Juice Shop**. This project showcases high-performance testing strategies, clean architecture, and custom tooling.

## 🌟 Highlights

- **Hybrid Automation**: Seamlessly combines UI and API testing. Uses API for fast state setup and teardown.
- **Custom Tooling**: Powered by **[SimpleApiPlaywright](https://github.com/notNullThen/simple-api-playwright-nuget-dotnet)**, a _custom NuGet package I developed_ for unified API/UI synchronization and context management.
- **Enterprise Patterns**: Implements Page Objects, Reusable Components, Data Builders, and `AsyncLocal` context management.
- **CI/CD Ready**: Fully dockerized execution with multi-stage builds and automated Allure reporting.
- **Strict Engineering**: Configured with `.editorconfig`, CSharpier, and strict linting.

## 🛠 Tech Stack

- **Lanuage**: C# / .NET
- **Engine**: Playwright
- **Runner**: xUnit
- **Infrastructure**: Docker & Docker Compose
- **Reporting**: Allure Framework
- **SimpleApiPlaywright** is now [published on NuGet](https://www.nuget.org/packages/SimpleApiPlaywright/)!

For documentation and source code, visit the [GitHub repository](https://github.com/notNullThen/simple-api-playwright-nuget-dotnet).

## ⚡ Quick Start (Docker)

Launch the entire ecosystem (Juice Shop + Tests + Reporting) with a single command:

```bash
docker compose up playwright --build
```

*The report will be available at [http://localhost:8080](http://localhost:8080) after execution.*

## 📂 Project Structure

- `OwaspPlaywrightTests/Tests/`: Structured API and UI test suites.
- `OwaspPlaywrightTests/ApiEndpoints/`: Strong-typed endpoint definitions.
- `OwaspPlaywrightTests/Pages/`: Clean Page Object implementation.
- `OwaspPlaywrightTests/Components/`: Reusable UI atomics (Buttons, Inputs).

---

### 🔗 Related
- [SimpleApiPlaywright](https://github.com/notNullThen/simple-api-playwright-nuget-dotnet) (My custom library for unified API/UI automation)
- [NodeJS/TypeScript Version](https://github.com/notNullThen/owasp-playwright-api-typescript-docker)

*Built by [notNullThen](https://github.com/notNullThen) to demonstrate modern SDET craftsmanship.*
