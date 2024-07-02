using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Io;
using DfE.Data.SearchPrototype.Test.PageModels;
using DfE.Data.SearchPrototype.Test.Shared;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Testing;
using WireMock.ResponseBuilders;

namespace Dfe.Data.SearchPrototype.Web.Acceptance.Tests.StepDefinitions;

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

    [StepDefinition(@"I (navigate to|am on) the (home|privacy) page")]
    public async Task NavigateToThePage(string action, string pageName)
    {
        _response = await NavigateToPageAsync(_pageNameToUrlConverter[pageName]);
    }

    [Then(@"The page heading is ""(.*)""")]
    public void PageHeadingIsExpected(string headingText)
    {
        // assert
        Assert.Equal("Welcome", _response.Heading().InnerHtml);
    }

    [StepDefinition(@"I can locate the Privacy link in the header")]
    public void LocatePrivacyLink()
    {
        IHtmlAnchorElement privacyLink =
        _response
            .Header()
            .AnchorTagWithName("Privacy");

        Assert.Equal("/Home/Privacy", privacyLink.PathName);

    }

    [StepDefinition(@"The Privacy link takes me to the privacy page")]
    public async Task PrivacyLink_SuccessfullyOpensPrivacyPage()
    {
        IHtmlAnchorElement privacyLink =
        _response
            .Header()
            .AnchorTagWithName("Privacy");

        var privacyPage = await NavigateToPageAsync(privacyLink.Href);

        Assert.Equal("Privacy Policy", privacyPage.Title);
    }
}
