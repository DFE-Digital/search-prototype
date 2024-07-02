using DfE.Data.SearchPrototype.Test.PageModels;
using DfE.Data.SearchPrototype.Test.Shared;
using DfE.Data.SearchPrototype.Web.Tests.Integration.PageModels;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DfE.Data.SearchPrototype.Web.Tests.Integration;

/// <summary>
/// test class using the new page model way for traversing the DOM
/// </summary>
public class HomePageTestsUsingPageModel : PageTestHelper
{
    public HomePageTestsUsingPageModel(WebApplicationFactory<Program> factory) : base(factory)
    {
    }

    [Fact]
    public async Task HomePage_ContainsExpectedTitle()
    {
        // act
        var homePage = new HomePage(await NavigateToPageAsync(HomePage.Url));

        // assert
        Assert.Equal("Welcome", homePage.Title);
    }

    [Fact]
    public async Task HomePage_ContainsLinkToPrivacyPage()
    {
        // act
        var homePage = new HomePage(await NavigateToPageAsync(HomePage.Url));

        var privacyPageLink = homePage.Header.PrivacyLink.Href;

        // assert
        Assert.Equal(PrivacyPage.Url, homePage.Header.PrivacyLink.Href);
    }

    [Fact]
    public async Task HomePage_PrivacyLink_GoesToPrivacyPage()
    {
        // act
        var homePage = new HomePage(await NavigateToPageAsync(HomePage.Url));

        var privacyPageLink = homePage.Header.PrivacyLink.Href;

        var privacyPage = new PrivacyPage(await NavigateToPageAsync(privacyPageLink!));

        // assert
        Assert.Equal("Privacy Policy", privacyPage.Title);
    }
}
