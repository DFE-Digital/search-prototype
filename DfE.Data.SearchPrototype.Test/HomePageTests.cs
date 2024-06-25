using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using DfE.Data.SearchPrototype.Test.Shared;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DfE.Data.SearchPrototype.Test;

public class HomePageTests : PageTestHelper
{
    public HomePageTests(WebApplicationFactory<Program> factory) : base(factory)
    {
    }

    [Fact]
    public async Task HomePage_ContainsExpectedTitle()
    {
        // act
        var response = await NavigateToPage("");

        // assert
        var headings = response.GetElementsByTagName("h1");
        Assert.Equal("Welcome", headings.First().InnerHtml);
    }

    [Fact]
    public async Task HomePage_ContainsPrivacyLink()
    {
        // act
        IDocument response = await NavigateToPage("");

        // Assert
        IHtmlAnchorElement privacyLink = response.GetHeaderLink("Privacy");
        Assert.Equal("/Home/Privacy", privacyLink.PathName);
    }
}
