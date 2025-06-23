# Agent Guidelines for Playwright C# SpecFlow Framework

This document provides guidelines for AI agents working with this codebase.

## General Principles

1.  **Understand the Structure**: Familiarize yourself with the framework structure outlined in `README.md`. Key components are in `PlaywrightSpecFlowFramework.Core` and tests reside in `PlaywrightSpecFlowFramework.Tests`.
2.  **Adhere to Design Patterns**:
    *   **Page Object Model (POM)**: When adding UI interactions, create or update Page Objects in `PlaywrightSpecFlowFramework.Tests/PageObjects/` (or `PlaywrightSpecFlowFramework.Core/PageObjects/` if truly generic). Pages should inherit from `BasePage`.
    *   **BDD with SpecFlow**: Write new tests using Gherkin syntax in `.feature` files under `PlaywrightSpecFlowFramework.Tests/Features/`. Implement corresponding steps in `PlaywrightSpecFlowFramework.Tests/StepDefinitions/`.
3.  **Configuration**:
    *   Test environment configurations (URLs, browser settings) are managed in `PlaywrightSpecFlowFramework.Tests/appsettings.json`.
    *   SpecFlow configurations are in `PlaywrightSpecFlowFramework.Tests/specflow.json`.
4.  **Idempotency**: Ensure that scripts and commands you run are idempotent where possible, or that their effects are understood if run multiple times.

## Modifying Code

1.  **Core Framework Changes (`PlaywrightSpecFlowFramework.Core`)**:
    *   Modifications to `DriverFactory`, `ConfigurationManager`, or `BasePage` should be done carefully, considering their impact on all tests.
    *   New generic utilities can be added to `PlaywrightSpecFlowFramework.Core/Utilities/`.
2.  **Test Project Changes (`PlaywrightSpecFlowFramework.Tests`)**:
    *   **New Features**:
        1.  Define scenarios in a new or existing `.feature` file.
        2.  Implement step definitions. Use existing step definitions if applicable.
        3.  Create new Page Objects or update existing ones for the UI elements involved.
        4.  Use `ScenarioContext` for sharing state within a single scenario. For more complex state sharing, consider constructor dependency injection provided by SpecFlow.
    *   **Locators**: Prefer robust locators (e.g., IDs, `data-testid`, CSS selectors). Avoid XPath if simpler alternatives exist, and avoid brittle, generated selectors.
    *   **Assertions**: Use FluentAssertions for readable and expressive assertions in step definitions.
    *   **Waits**: Utilize Playwright's auto-waiting capabilities. For explicit waits, use methods available on `IPage` or `ILocator` (e.g., `WaitForSelectorAsync`, `WaitForLoadStateAsync`). Avoid fixed `Task.Delay()` calls.
3.  **Dependencies**:
    *   If adding new NuGet packages, add them to the appropriate `.csproj` file (`.Core` or `.Tests`).
    *   Run `dotnet restore` if you modify project files.
4.  **Code Style**:
    *   Follow standard C# coding conventions.
    *   Keep methods short and focused.
    *   Add comments for complex logic or non-obvious decisions.

## Running and Verifying

1.  **Build**: Always ensure the solution builds successfully after your changes:
    ```bash
    dotnet build PlaywrightSpecFlowFramework.sln
    ```
2.  **Test Execution**: Run relevant tests to verify your changes. If you add new tests, ensure they pass. If you modify existing functionality, ensure related tests still pass.
    ```bash
    dotnet test PlaywrightSpecFlowFramework.sln
    ```
    Consider running specific features or scenarios if the full test suite is large:
    ```bash
    dotnet test --filter "FullyQualifiedName~YourFeatureOrScenarioName"
    ```
3.  **Playwright Browsers**: Remember that Playwright browsers need to be installed. If you see errors related to browser executables, run `playwright install`.

## Committing Changes

1.  **Branching**: Use a descriptive branch name for your changes.
2.  **Commit Messages**: Follow standard commit message conventions (e.g., imperative mood, short subject line, detailed body if necessary).
3.  **Submit**: Use the `submit` tool when your changes are complete, tested, and meet the requirements.

## Environment Issues

*   The development environment for this agent currently has issues with `dotnet` command availability. Any `dotnet` commands in instructions are for the end-user's environment or a correctly configured agent environment. When generating code, assume a working .NET 8 environment.
*   File paths are relative to the repository root unless specified otherwise.

By following these guidelines, you can contribute effectively to this framework. If instructions conflict with the user's direct request, the user's request takes precedence. If anything is unclear, ask for clarification.Tool output for `create_file_with_block`:
