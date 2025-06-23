using Microsoft.Playwright;
using PlaywrightSpecFlowFramework.Core.Utilities;
using System;
using System.Threading.Tasks;

namespace PlaywrightSpecFlowFramework.Core.WebDriver
{
    public class DriverFactory
    {
        private IPlaywright? _playwright;
        private IBrowser? _browser;
        private IPage? _page;
        private readonly TestSettings _settings;

        public DriverFactory()
        {
            _settings = ConfigurationManager.AppSettings;
        }

        public async Task<IPage> InitializeAsync()
        {
            _playwright = await Playwright.CreateAsync();

            var launchOptions = new BrowserTypeLaunchOptions
            {
                Headless = _settings.Headless,
                SlowMo = _settings.SlowMo,
                DefaultTimeout = _settings.DefaultTimeout
            };

            _browser = _settings.BrowserType.ToLowerInvariant() switch
            {
                "firefox" => await _playwright.Firefox.LaunchAsync(launchOptions),
                "webkit" => await _playwright.Webkit.LaunchAsync(launchOptions),
                "chromium" => await _playwright.Chromium.LaunchAsync(launchOptions),
                _ => throw new ArgumentException($"Unsupported browser type: {_settings.BrowserType}"),
            };

            var contextOptions = new BrowserNewContextOptions();
            if (_settings.Viewport != null)
            {
                contextOptions.ViewportSize = new ViewportSize
                {
                    Width = _settings.Viewport.Width,
                    Height = _settings.Viewport.Height
                };
            }

            // Add other context options if needed, e.g., BaseURL
            // contextOptions.BaseURL = _settings.BaseUrl; // BaseURL for navigation methods like page.GotoAsync("/")

            var browserContext = await _browser.NewContextAsync(contextOptions);
            browserContext.SetDefaultTimeout(_settings.DefaultTimeout);
            browserContext.SetDefaultNavigationTimeout(_settings.DefaultTimeout);

            _page = await browserContext.NewPageAsync();
            return _page;
        }

        public async Task CloseAsync()
        {
            if (_page != null && !_page.IsClosed)
            {
                await _page.CloseAsync();
            }
            if (_browser != null)
            {
                await _browser.CloseAsync();
            }
            _playwright?.Dispose();
        }

        public IPage? GetPage() => _page;
    }
}
