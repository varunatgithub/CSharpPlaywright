using Microsoft.Playwright;
using PlaywrightSpecFlowFramework.Core.PageObjects; // Import BasePage
using System.Threading.Tasks;

namespace PlaywrightSpecFlowFramework.Tests.PageObjects
{
    public class HomePage : BasePage
    {
        // Define locators for elements on the Home page
        // Example: public ILocator SomeElement => Page.Locator("#someElementId");

        public HomePage(IPage page) : base(page)
        {
        }

        public async Task NavigateToHomePageAsync()
        {
            // BaseUrl is already handled by ConfigurationManager and BasePage.NavigateAsync
            // We can navigate to a specific path relative to BaseUrl if needed,
            // or ensure BaseUrl itself is the homepage.
            // For this example, BaseUrl in appsettings.json points to playwright.dev
            await NavigateAsync(); // Navigates to BaseUrl
        }

        // Add methods for interactions specific to the Home page
        // Example:
        // public async Task ClickSomeElementAsync()
        // {
        //     await SomeElement.ClickAsync();
        // }
    }
}
