using AngleSharp;
using AngleSharp.Dom;
using DfE.Data.SearchPrototype.Test.Shared;
using Microsoft.AspNetCore.Mvc.Testing;

namespace SpecFlow.Specs.StepDefinitions;

[Binding]
public class HomePageStepDefinitions : PageTestHelper
{
    private IDocument _response { get; set; } = null!;

    public HomePageStepDefinitions(WebApplicationFactory<Program> factory) : base(factory)
    {
    }

    [When(@"I navigate to ""(.*)""")]
    public async Task NavigateTo(string page)
    {
        _response = await NavigateToPage(page);
    }

    [Then(@"The page heading is ""(.*)""")]
    public void PageHeadingIsExpected(string headingText)
    {
        // assert
        Assert.Equal(headingText, _response.Heading().InnerHtml);
    }
}
