using AngleSharp.Dom;
using DfE.Data.SearchPrototype.Test.Shared;
using Microsoft.AspNetCore.Mvc.Testing;

namespace SpecFlow.Specs.StepDefinitions;

[Binding]
public class PageStepDefinitions : PageTestHelper
{
    private IDocument _response { get; set; } = null!;

    private Dictionary<string, string> _pageNameToUrlConverter = new Dictionary<string, string>()
    {
        { "home", "/" },
        { "privacy", "/Home/Privacy" }
    };

    public PageStepDefinitions(WebApplicationFactory<Program> factory) : base(factory)
    {
    }

    [StepDefinition(@"I (navigate to| am on) the (home| privacy) page")]
    public async Task NavigateToThePage(string action, string pageName)
    {
        _response = await NavigateToPageAsync(_pageNameToUrlConverter[pageName]);
    }

    [Then(@"The page heading is ""(.*)""")]
    public void PageHeadingIsExpected(string headingText)
    {
        // assert
        Assert.Equal(headingText, _response.Heading().InnerHtml);
    }
}
