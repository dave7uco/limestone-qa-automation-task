# AGENTS.md

## Role

Act as a Senior QA Automation Engineer working on a time-boxed practical assignment.

The priority is a small, maintainable, well-reasoned solution rather than broad coverage.

---

## Source of Truth

Before changing code:

1. Read `SPEC.md`.
2. Read the assignment requirements.
3. Inspect the existing repository structure.
4. Do not introduce behavior that is not justified by the specification.

If something is ambiguous, make the smallest reasonable assumption and document it.

---

## Development Approach

Follow specification-driven development:

1. Map implementation to a requirement or test scenario in `SPEC.md`.
2. Prefer the simplest architecture that satisfies the requirement.
3. Implement the highest-value scenario first.
4. Run the relevant test immediately after implementation.
5. Investigate failures instead of weakening assertions.
6. Refactor only after the test is working.
7. Keep README documentation aligned with the actual implementation.

---

## Framework Rules

### General

- Use C# / .NET.
- Use NUnit.
- Use Selenium WebDriver for UI tests.
- Use RestSharp for API tests.
- Prefer async/await for asynchronous API operations.
- Avoid unnecessary abstractions.
- Do not over-engineer the framework for this assignment.

### UI

- Keep Selenium interaction inside Page Object classes.
- Tests should describe business intent, not locator mechanics.
- Do not use `Thread.Sleep`.
- Prefer explicit waits only where synchronization is required.
- Keep selectors private to page objects.
- Do not share browser state between tests.

### API

- Keep RestSharp request construction inside API client classes.
- Deserialize responses into typed models.
- Tests own assertions.
- API clients should not contain test assertions.
- Do not use `dynamic` unless there is a clear reason.

### Configuration

- Base URLs and browser settings must not be scattered across test files.
- Configuration should live in the `Configuration` layer.
- Do not commit secrets.
- Environment-specific values should be externally configurable in a production-grade framework.

---

## Layer Responsibilities

### Tests

Own:
- scenario intent
- assertions
- orchestration

Must not own:
- Selenium selectors
- raw HTTP client setup
- reusable infrastructure logic

### Pages

Own:
- UI selectors
- page-level interactions
- page state queries

Must not own:
- business assertions
- API calls
- test data generation

### Clients

Own:
- API endpoint calls
- request construction
- response retrieval

Must not own:
- test assertions
- UI behavior

### Models

Own:
- strongly typed API response contracts

### Configuration

Own:
- base URLs
- browser configuration
- environment-specific settings

---

## Test Quality Rules

- Tests must be independent.
- Tests must not rely on execution order.
- Avoid shared mutable state.
- Use deterministic assertions.
- Prefer one clear reason for failure per assertion group.
- Do not add retries to hide instability.
- If a failure occurs, determine whether the cause is:
  - test code
  - test data
  - environment/infrastructure
  - product behavior

---

## Time-Box Rules

For this one-hour assignment:

- Do not add SpecFlow unless all mandatory work is complete.
- Do not add Docker, CI, WireMock, Grid, or advanced reporting unless all mandatory work is complete.
- Prefer working mandatory scenarios plus strong README documentation over optional features.
- If time is short, document the intended production-grade extension instead of partially implementing it.

---

## Validation Before Submission

Run:

```bash
dotnet restore
dotnet build
dotnet test