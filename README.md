# Limestone QA Automation Task

## Overview

This repository contains a focused QA automation submission built with C# and .NET 10. NUnit is the test framework, Selenium WebDriver automates UI scenarios against [SauceDemo](https://www.saucedemo.com/), and RestSharp validates API behavior against [JSONPlaceholder](https://jsonplaceholder.typicode.com/).

The solution favors maintainability, readable tests, and clear separation of concerns over broad coverage. It implements only the mandatory scenarios appropriate for the one-hour assignment.

## Prerequisites

- .NET 10 SDK
- Google Chrome
- Git
- Postman (optional, for importing and running the exported collection)

## Install

From the repository root:

~~~powershell
cd Limestone.Automation.Tests
dotnet restore
dotnet build
~~~

## Run tests

Run these commands from Limestone.Automation.Tests.

Full suite:

~~~powershell
dotnet test
~~~

Single UI test:

~~~powershell
dotnet test --filter "FullyQualifiedName=Limestone.Automation.Tests.Tests.UI.SauceDemoTests.ValidUserCanLogIn"
~~~

Single API test:

~~~powershell
dotnet test --filter "FullyQualifiedName=Limestone.Automation.Tests.Tests.API.JsonPlaceholderTests.GetUserReturnsExpectedUser"
~~~

## Test results

NUnit results, failures, and stack traces are displayed in the dotnet test console output. A TRX result can also be produced with:

~~~powershell
dotnet test --logger "trx;LogFileName=test-results.trx"
~~~

Custom HTML reporting was not added because of the one-hour time limit. A production framework would publish TRX, JUnit, or HTML reports and supporting diagnostic artifacts through CI.

## Project structure

~~~text
Limestone.Automation.Tests/
├── Tests/
│   ├── UI/                 # NUnit SauceDemo scenarios and assertions
│   └── API/                # NUnit JSONPlaceholder scenarios and assertions
├── Pages/                  # Selenium locators, interactions, and page-state queries
├── Clients/                # RestSharp request construction and execution
├── Models/                 # Strongly typed API response contracts
├── Configuration/          # Centralized URLs, browser, and demo credentials
└── Limestone.Automation.Tests.csproj
postman/                    # Importable Postman collection and environment
sql/                        # Northwind SQL query
SPEC.md                     # Test scope and requirements
AGENTS.md                   # Engineering and implementation guidance
README.md
~~~

## Design decisions

- Page Object Model keeps Selenium selectors and interactions out of tests.
- A dedicated RestSharp client isolates HTTP request construction.
- Typed response models make API contracts explicit.
- NUnit tests own scenario orchestration and assertions.
- URLs, browser choice, and public demo credentials are centralized.
- Each UI test creates and disposes its own ChromeDriver through NUnit SetUp and TearDown.
- Explicit waits are used where synchronization is needed; there is no Thread.Sleep.
- Factories, dependency injection, retries, BDD, and other abstractions were intentionally omitted to avoid over-engineering the time-boxed solution.

## Framework structure for a long-lived suite

### 1. Layers and ownership

- **Tests:** express scenarios, orchestrate components, and own assertions.
- **Pages:** own UI selectors, page interactions, and page-state queries.
- **API Clients:** own endpoint paths, HTTP request construction, and response retrieval.
- **Models/Contracts:** represent request and response payloads with strong types.
- **Configuration:** resolves environments, URLs, browser settings, timeouts, and feature options.
- **Test Data:** provides isolated fixtures, builders, and cleanup for deterministic scenarios.
- **Infrastructure:** owns driver creation, shared NUnit fixtures, remote execution, logging, and cross-cutting test utilities.
- **Reporting:** collects test outcomes and diagnostic artifacts without adding assertions or business behavior.

Layer boundaries must remain strict: selectors must not leak into tests; assertions must not leak into page objects or API clients; raw HTTP implementation must not leak into tests; and environment-specific secrets must never be hard-coded.

### 2. Driver lifecycle and browser execution

The current submission creates one independent ChromeDriver for each UI test in NUnit SetUp and quits/disposes it in TearDown. This keeps browser state isolated.

For a production suite, driver creation would move into a small DriverFactory or shared fixture. Browser choice and capabilities would be external configuration. The same tests could then run locally, in containers, or on Selenium Grid through RemoteWebDriver without page or test code changes.

### 3. Environment configuration and secrets

The current public demo URLs, browser choice, and SauceDemo credentials are centralized in one configuration class. In a real framework, settings would come from appsettings files and environment variables, with the target environment selected externally.

CI credentials and tokens would be held in an approved secret-management system and injected at runtime, never committed. Tests should run against different environments without source-code changes.

### 4. Page objects, step definitions, test data, and API clients

Page objects should contain UI mechanics and expose intention-revealing actions. API clients should contain transport details and expose endpoint operations. Test-data builders or fixtures should create scenario-specific data independently of both layers. Tests should combine these components and make the assertions.

If business-readable BDD provides clear value, SpecFlow step definitions could be added as a thin orchestration layer above pages, clients, and test data. Step definitions should not duplicate selectors, raw HTTP code, or infrastructure behavior.

### 5. Parallelism and independence

Every test should own its browser, client context, and test data, with no execution-order dependency or shared mutable driver/client state. Test data should use unique identifiers and reliable cleanup. Parallel execution should be enabled at the test-runner level only when the application, environment, and data strategy safely support it.

### 6. Failure reporting and investigation

Failures should preserve stack traces, structured logs, and relevant context. UI failures should capture screenshots and useful browser details. API failures should record sanitized request and response metadata while excluding credentials, tokens, and sensitive payloads.

Investigation should classify a failure as a test defect, test-data issue, environment/infrastructure issue, or product defect. Blind reruns and retries should not be the default response because they can conceal real instability.

### 7. CI integration and blocking policy

A pull-request pipeline should run fast smoke and critical-path tests, while nightly or scheduled jobs run broader regression coverage. CI should publish test reports, screenshots, logs, and other diagnostics.

Critical deterministic failures may block merging. Flaky or non-critical tests should be stabilized before they become blocking. Optional test stages can remain non-blocking according to risk. Tests that mutate production-like data require explicit safety controls, restricted permissions, isolated records, and cleanup before they are allowed in automated pipelines.

## What exists in this submission

**Implemented:**

- NUnit
- Selenium WebDriver
- Page Objects
- RestSharp
- Typed API response models
- Centralized configuration
- Postman collection and environment
- SQL query
- SPEC.md
- AGENTS.md

**Description only / not implemented:**

- Docker
- Selenium Grid
- SpecFlow
- WireMock
- CI workflow
- Advanced reporting
- External secret management
- Parallel execution configuration
- Richer schema validation

## Improvements with more time

Prioritized next steps:

1. Externalize environment and browser configuration.
2. Add a shared driver factory and NUnit fixture infrastructure.
3. Capture richer diagnostics and publish test reports.
4. Add a CI pipeline with risk-based blocking rules.
5. Configure safe parallel execution and isolated test data.
6. Add richer API schema validation.
7. Add Docker and Selenium Grid support where execution scale requires them.
8. Optionally add SpecFlow if business-readable BDD provides measurable value.

## Postman

The exported files are:

- postman/Limestone-QA.postman_collection.json
- postman/Limestone-QA.postman_environment.json

Import both files into Postman, select the **Limestone QA** environment, and run the collection. The environment supplies baseUrl for both JSONPlaceholder requests.

## SQL

The Northwind query is stored at sql/query.sql. It returns distinct customer names and countries for orders shipped by United Package.

## Assumptions / limitations

- SauceDemo and JSONPlaceholder are public demo services and are assumed to be available.
- The current implementation supports Chrome only.
- UI tests launch a visible browser; headless execution is not configured.
- Tests depend on live external services and can be affected by network or service availability.
- No advanced reporting is implemented beyond NUnit/dotnet output and optional built-in loggers.
- The committed SauceDemo credentials are public test credentials, not real secrets; real credentials must be externally managed.
