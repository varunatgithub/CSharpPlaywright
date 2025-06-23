using Microsoft.Playwright;
using PlaywrightSpecFlowFramework.Core.WebDriver;
using TechTalk.SpecFlow;
using System.Threading.Tasks;
using PlaywrightSpecFlowFramework.Core.Utilities; // For TestSettings if needed directly

namespace PlaywrightSpecFlowFramework.Tests.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        private readonly ScenarioContext _scenarioContext;
        private static DriverFactory? _driverFactory; // Static to share across scenarios if needed, or instance per scenario
        private static TestSettings? _testSettings;

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            // Initialize Playwright (ensure browsers are downloaded)
            // This can be done here or rely on Playwright's auto-download.
            // For explicit control:
            // Microsoft.Playwright.Program.Main(new[] { "install" });
            // Requires Microsoft.Playwright.CLI to be referenced or accessible.

            _testSettings = ConfigurationManager.AppSettings; // Load settings once
            // Initialize reporting, logging, etc. here if needed
        }

        [BeforeScenario]
        public async Task BeforeScenario()
        {
            _driverFactory = new DriverFactory();
            var page = await _driverFactory.InitializeAsync();
            _scenarioContext.Set(page, "CurrentPage");
            _scenarioContext.Set(_driverFactory, "DriverFactory");
        }

        [AfterScenario]
        public async Task AfterScenario()
        {
            if (_scenarioContext.TryGetValue("DriverFactory", out DriverFactory? driverFactory) && driverFactory != null)
            {
                // Take screenshot on failure
                if (_scenarioContext.TestError != null)
                {
                    var page = _scenarioContext.Get<IPage>("CurrentPage");
                    var screenshotDir = _testSettings?.ScreenshotPath ?? "Screenshots";
                    if (!System.IO.Directory.Exists(screenshotDir))
                    {
                        System.IO.Directory.CreateDirectory(screenshotDir);
                    }
                    var scenarioTitle = _scenarioContext.ScenarioInfo.Title.Replace(" ", "_");
                    var filePath = System.IO.Path.Combine(screenshotDir, $"{scenarioTitle}_Error.png");
                    await page.ScreenshotAsync(new PageScreenshotOptions { Path = filePath });
                    // Log screenshot path or attach to report
                }

                await driverFactory.CloseAsync();
            }
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            // Clean up resources, close reports
        }
    }
}
