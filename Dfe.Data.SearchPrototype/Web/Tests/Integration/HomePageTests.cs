using AngleSharp.Html.Dom;
using DfE.Data.SearchPrototype.Web.Tests.Integration.PageObjectModel;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace DfE.Data.SearchPrototype.Web.Tests.Integration;

public class HomePageTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _webApplicationFactory;

    public HomePageTests(WebApplicationFactory<Program> webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;
    }

    [Fact]
    public async Task HomePage_ContainsExpectedTitle()
    {
        // act
        var searchHeader = new SearchHeader(_webApplicationFactory);

        // assert
        string searchHeading = await searchHeader.GetHeading();
        Assert.Equal("Welcome", searchHeading);
    }

    [Fact]
    public async Task HomePage_ContainsPrivacyLink()
    {
        // act
        var searchHeader = new SearchHeader(_webApplicationFactory);

        // Assert
        IHtmlAnchorElement privacyLink = await searchHeader.GetSearchHeaderLink("Privacy");
        Assert.Equal("/Home/Privacy", privacyLink.PathName);
    }
}
