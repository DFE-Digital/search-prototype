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
    public void HomePage_ContainsExpectedTitle()
    {
        string searchHeading =_homePage.GetHomePageHeading();

        Assert.Equal("Search prototype", searchHeading);
    }

    [Fact]
    public void HomePage_ContainsPrivacyLink()
    {
        IHtmlAnchorElement privacyLink = _homePage.GetHomePageHeaderLink();
 
        Assert.Equal("/Home/Privacy", privacyLink.PathName);
    }
}
