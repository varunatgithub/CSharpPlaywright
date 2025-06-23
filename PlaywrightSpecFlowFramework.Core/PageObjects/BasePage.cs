using Microsoft.Playwright;
using System.Threading.Tasks;
using PlaywrightSpecFlowFramework.Core.Utilities;


namespace PlaywrightSpecFlowFramework.Core.PageObjects
{
    public abstract class BasePage
    {
        protected readonly IPage Page;
        protected readonly TestSettings Settings;
        private readonly string _baseUrl;

        protected BasePage(IPage page)
        {
            Page = page;
            Settings = ConfigurationManager.AppSettings;
            _baseUrl = Settings.BaseUrl ?? string.Empty;
        }

        public virtual async Task NavigateAsync(string relativeUrl = "")
        {
            string urlToNavigate = _baseUrl;
            if (!string.IsNullOrEmpty(relativeUrl))
            {
                // Ensure no double slashes if BaseUrl ends with / and relativeUrl starts with /
                if (urlToNavigate.EndsWith("/") && relativeUrl.StartsWith("/"))
                {
                    urlToNavigate = urlToNavigate + relativeUrl.Substring(1);
                }
                else if (!urlToNavigate.EndsWith("/") && !relativeUrl.StartsWith("/"))
                {
                     urlToNavigate = urlToNavigate + "/" + relativeUrl;
                }
                else
                {
                    urlToNavigate = urlToNavigate + relativeUrl;
                }
            }

            await Page.GotoAsync(urlToNavigate);
        }

        public async Task<string> GetTitleAsync()
        {
            return await Page.TitleAsync();
        }

        public async Task TakeScreenshotAsync(string fileName)
        {
            var screenshotDir = Settings.ScreenshotPath ?? "Screenshots";
            if (!System.IO.Directory.Exists(screenshotDir))
            {
                System.IO.Directory.CreateDirectory(screenshotDir);
            }
            await Page.ScreenshotAsync(new PageScreenshotOptions { Path = System.IO.Path.Combine(screenshotDir, $"{fileName}.png") });
        }

        // Common helper methods can be added here
        protected ILocator Locator(string selector, PageLocatorOptions? options = null)
            => Page.Locator(selector, options);

        protected async Task ClickAsync(string selector, PageClickOptions? options = null)
            => await Page.ClickAsync(selector, options);

        protected async Task FillAsync(string selector, string value, PageFillOptions? options = null)
            => await Page.FillAsync(selector, value, options);

        protected async Task<string?> GetTextAsync(string selector, PageInnerTextOptions? options = null)
            => await Page.InnerTextAsync(selector, options);

        protected async Task WaitForSelectorAsync(string selector, PageWaitForSelectorOptions? options = null)
            => await Page.WaitForSelectorAsync(selector, options);
    }
}
