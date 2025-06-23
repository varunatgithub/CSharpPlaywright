using FluentAssertions;
using Microsoft.Playwright;
using PlaywrightSpecFlowFramework.Tests.PageObjects;
using TechTalk.SpecFlow;
using System.Threading.Tasks;

namespace PlaywrightSpecFlowFramework.Tests.StepDefinitions
{
    [Binding]
    public class ExampleStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IPage _page;
        private HomePage _homePage; // Instance of the Page Object

        public ExampleStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            // Get the IPage instance from ScenarioContext (set in Hooks)
            _page = _scenarioContext.Get<IPage>("CurrentPage");
            _homePage = new HomePage(_page);
        }

        [Given(@"I am on the Playwright homepage")]
        public async Task GivenIAmOnThePlaywrightHomepage()
        {
            await _homePage.NavigateToHomePageAsync();
        }

        [When(@"I get the title of the page")]
        public async Task WhenIGetTheTitleOfThePage()
        {
            var title = await _homePage.GetTitleAsync();
            _scenarioContext["PageTitle"] = title; // Store title for verification in Then step
        }

        [Then(@"the title should contain ""(.*)""")]
        public void ThenTheTitleShouldContain(string expectedSubstring)
        {
            var pageTitle = _scenarioContext.Get<string>("PageTitle");
            pageTitle.Should().Contain(expectedSubstring);
        }

        [Then(@"the title should be ""(.*)""")]
        public void ThenTheTitleShouldBe(string expectedTitle)
        {
            var pageTitle = _scenarioContext.Get<string>("PageTitle");
            pageTitle.Should().Be(expectedTitle);
        }
    }
}
