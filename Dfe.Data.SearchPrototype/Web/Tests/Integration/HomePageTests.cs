using AngleSharp.Html.Dom;
using DfE.Data.SearchPrototype.Web.Tests.Integration.PageObjectModel;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace DfE.Data.SearchPrototype.Web.Tests.Integration;

public class HomePageTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HomePage _homePage;

    public HomePageTests(WebApplicationFactory<Program> webApplicationFactory)
    {
        _homePage = HomePage.Create(webApplicationFactory);
    }

    [Fact]
    public async Task HomePage_ContainsExpectedTitle()
    {
        // assert
        string searchHeading = await _homePage.PageHeader.GetHeading();
        Assert.Equal("Welcome", searchHeading);
    }

    [Fact]
    public async Task HomePage_ContainsPrivacyLink()
    {
        // Assert
        IHtmlAnchorElement privacyLink = await _homePage.PageHeader.GetSearchHeaderLink("Privacy");
        Assert.Equal("/Home/Privacy", privacyLink.PathName);
    }
}
