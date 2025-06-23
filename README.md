# Playwright C# SpecFlow Automation Framework

This project provides a production-ready automation framework using .NET 8, C#, SpecFlow, and Playwright. It is designed with best practices for scalability and robustness.

## Prerequisites

1.  **.NET 8 SDK**: Ensure you have the .NET 8 SDK installed. You can download it from [here](https://dotnet.microsoft.com/download/dotnet/8.0).
2.  **Playwright Browsers**: Playwright requires browsers to be installed. The framework attempts to manage this, but if you encounter issues, you might need to install them manually.
    After restoring the .NET packages (which includes `Microsoft.Playwright`), run the following command in your terminal (preferably in the repository root or where the solution file is):
    ```bash
    playwright install
    ```
    Alternatively, the `Microsoft.Playwright.CLI` package (included as a dependency) allows you to run this via:
    ```bash
    # Install the CLI tool if you haven't already
    # dotnet tool install --global Microsoft.Playwright.CLI
    # Then run install
    playwright install
    ```
    The build process itself might trigger the download if Playwright's MSBuild tasks are correctly configured and run. The `Hooks.cs` file also contains a commented-out line (`// Microsoft.Playwright.Program.Main(new[] { "install" });`) that can be enabled for programmatic installation at runtime if needed.

## Setup

1.  **Clone the repository**:
    ```bash
    git clone <repository-url>
    cd <repository-name>
    ```
2.  **Restore .NET dependencies**:
    From the root directory of the solution (where `PlaywrightSpecFlowFramework.sln` is located):
    ```bash
    dotnet restore PlaywrightSpecFlowFramework.sln
    ```
3.  **Build the solution**:
    ```bash
    dotnet build PlaywrightSpecFlowFramework.sln
    ```
    This step will also generate C# code-behind files for your `.feature` files. If you encounter build issues, ensure `dotnet restore` was successful and that the .NET SDK is correctly configured.

## Configuration

The primary configuration file is `PlaywrightSpecFlowFramework.Tests/appsettings.json`. You can modify settings such as:
*   `BrowserType`: `Chromium`, `Firefox`, or `Webkit`.
*   `BaseUrl`: The base URL for the application under test.
*   `Headless`: `true` to run browsers in headless mode, `false` otherwise.
*   `SlowMo`: Adds a delay (in milliseconds) between Playwright operations for debugging.
*   `DefaultTimeout`: Default timeout for Playwright operations.
*   `Viewport`: Sets the browser window size.
*   `ScreenshotPath`: Directory to save screenshots (especially on test failure).

SpecFlow specific configurations can be found in `PlaywrightSpecFlowFramework.Tests/specflow.json`.

## Running Tests

Tests are run using the .NET test runner.

1.  **Navigate to the test project directory** (optional, can run from solution root too):
    ```bash
    cd PlaywrightSpecFlowFramework.Tests
    ```
    Or stay in the solution root.
2.  **Run tests**:
    From the solution root:
    ```bash
    dotnet test PlaywrightSpecFlowFramework.sln
    ```
    Or from the test project directory:
    ```bash
    dotnet test
    ```
    You can also run tests from Visual Studio's Test Explorer or other IDEs with .NET test integration.

## Framework Structure

*   **`PlaywrightSpecFlowFramework.sln`**: The solution file.
*   **`PlaywrightSpecFlowFramework.Core/`**: Contains the core framework components.
    *   `PageObjects/BasePage.cs`: Base class for all page objects, providing common functionalities.
    *   `Utilities/ConfigurationManager.cs`: Handles loading of test settings from `appsettings.json`.
    *   `Utilities/TestSettings.cs` (implicitly part of `ConfigurationManager.cs`): Defines the structure of settings in `appsettings.json`.
    *   `WebDriver/DriverFactory.cs`: Manages Playwright browser instances (creation, configuration, disposal).
*   **`PlaywrightSpecFlowFramework.Tests/`**: Contains the actual tests, features, and step definitions.
    *   `appsettings.json`: Configuration file for test execution.
    *   `specflow.json`: Configuration file for SpecFlow.
    *   `Features/`: Contains Gherkin feature files.
    *   `StepDefinitions/`: Contains C# step definition classes.
    *   `PageObjects/`: Contains page-specific object classes.
    *   `Hooks/Hooks.cs`: Contains SpecFlow hooks for setup/teardown logic (e.g., starting browser, taking screenshots on failure).

## Key Design Practices Implemented

*   **Page Object Model (POM)**: For maintainable and reusable UI interactions.
*   **Configuration Driven**: Test settings managed externally via `appsettings.json`.
*   **SpecFlow for BDD**: Behavior-Driven Development using Gherkin syntax.
*   **Playwright for Browser Automation**: Modern, fast, and reliable browser automation.
*   **Centralized Driver Management**: `DriverFactory` handles Playwright setup.
*   **Hooks for Lifecycle Management**: Manages setup and teardown routines for tests.
*   **Screenshot on Failure**: Automatically captures screenshots when tests fail.
*   **Scalable Project Structure**: Separate Core and Test projects for better organization.

## Troubleshooting

*   **`dotnet` command not found**: Ensure .NET SDK is installed and added to your system's PATH.
*   **Playwright browser issues**: Run `playwright install` if browsers are missing or not detected. Check Playwright documentation for specific errors. The first time you build or run, Playwright might attempt to download browsers, which can take time.
*   **Build errors related to SpecFlow**: Ensure `SpecFlow.Tools.MsBuild.Generation` package is correctly installed and that `dotnet build` is run to generate feature file code-behinds. Clean and rebuild if issues persist (`dotnet clean && dotnet build`).
*   **Test Discovery/Execution Issues**: Ensure NUnit3TestAdapter is correctly referenced. Check output for any errors from the test runner.

This framework provides a solid foundation. Further enhancements could include:
*   Advanced logging and reporting (e.g., Allure, ExtentReports).
*   CI/CD integration examples.
*   Parallel test execution setup (SpecFlow+NUnit supports this).
*   More complex utility functions (e.g., API helpers, data generators).
*   Dependency Injection for sharing state in SpecFlow if needed beyond `ScenarioContext`.
