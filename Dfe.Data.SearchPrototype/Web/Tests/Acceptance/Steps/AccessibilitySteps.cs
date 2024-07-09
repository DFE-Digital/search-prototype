using Deque.AxeCore.Selenium;
using DfE.Data.SearchPrototype.Web.Tests.Integration.PageObjectModel;
using FluentAssertions;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Xunit.Abstractions;

namespace Dfe.Data.SearchPrototype.Web.Tests.Acceptance.Steps
{
    [Binding]
    public sealed class AccessibilitySteps
    {
        private readonly HomePage _homePage;
        private readonly IWebDriver _driver;
        private readonly ITestOutputHelper _logger;
        private readonly ScenarioContext _scenarioContext;

        public AccessibilitySteps(
            HomePage homePage,
            IWebDriver driver,
            ITestOutputHelper logger,
            ScenarioContext scenarioContext
        )
        {
            _driver = driver;
            _homePage = homePage; 
            _logger = logger;
            _scenarioContext = scenarioContext;
        }

        [StepDefinition(@"the user views the homepage")]
        public void OpenHome()
        {
            _driver.Navigate().GoToUrl("http://localhost:5000/");
            _homePage.Heading.Criteria.Should().NotBeNull();
        }

        [StepDefinition(@"the (.*) is accessible")]
        public void IsAccessible(string component)
        {
            // see https://github.com/dequelabs/axe-core/blob/develop/doc/API.md#axe-core-tags

            var axeResult = new AxeBuilder(_driver)
               .WithTags("wcag2a", "wcag2aa", "wcag21a", "wcag21aa")
               .Analyze();

            _logger.WriteLine($"Scan completed");

            // Check that axe ran successfuly https://github.com/dequelabs/axe-core/blob/develop/doc/API.md#error-result
            axeResult.Violations.Should().BeEmpty();

            var passCount = axeResult.Passes.Length;
            passCount.Should().NotBe(0);
            _logger.WriteLine($"Passed accessibility test count {passCount} for {component} at {axeResult.Url}");
        }
    }
}
