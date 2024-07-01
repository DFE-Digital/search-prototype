using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using DfE.Data.SearchPrototype.Test.PageModels;
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
        var response = await NavigateToPageAsync("");

        // assert
        Assert.Equal("Welcome", response.Heading().InnerHtml);
    }

    [Fact]
    public async Task HomePage_HeaderContainsPrivacyLink()
    {
        // act
        IDocument response = await NavigateToPageAsync("");

        // Assert
        IHtmlAnchorElement privacyLink = 
            response
                .Header()
                .AnchorTagWithName("Privacy");

        Assert.Equal("/Home/Privacy", privacyLink.PathName);
    }
}
