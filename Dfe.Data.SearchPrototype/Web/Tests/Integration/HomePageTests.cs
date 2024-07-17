using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Dfe.Data.SearchPrototype.Web.Tests.PageObjectModel;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace DfE.Data.SearchPrototype.Web.Tests.Integration;

public class HomePageTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HomePage _homePage;
    private readonly WebApplicationFactory<Program> _webApplicationFactory;

    public HomePageTests(WebApplicationFactory<Program> webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;
        _homePage = HomePage.Create(webApplicationFactory);
    }

    [Fact]
    public void HomePage_ContainsExpectedTitle()
    {
        string searchHeading = _homePage.GetHomePageHeading();

        Assert.Equal("Search prototype", searchHeading);
    }

    [Fact]
    public void HomePage_ContainsPrivacyLink()
    {
        IHtmlAnchorElement privacyLink = _homePage.GetHomePagePrivacyLink();

        Assert.Equal("/Home/Privacy", privacyLink.PathName);
    }

    [Fact]
    public void HomePage_PrivacyLink_GoesToPrivacyPage()
    {
        IHtmlAnchorElement privacyLink = _homePage.GetHomePagePrivacyLink();

        var privacyPage = PrivacyPage.NavigateToPage(_webApplicationFactory, privacyLink.Href);

        Assert.Equal("Privacy Policy", privacyPage.GetPrivacyPageTitle());
    }

    [Fact]
    public async Task InvokeSearch_WithValidSearchString_ShowsSearchResults()
    {
        IHtmlInputElement searchBox = _homePage.GetSearchInputBox();
        // type into the box
        searchBox.Value = "Asia";

        // hit submit
        IDocument result = await _homePage.DocumentObjectModel.QuerySelector<IHtmlFormElement>("form").SubmitAsync();

        var docReqst = _homePage.DocumentObjectModel.QuerySelector<IHtmlFormElement>("form").GetSubmission();
        await _homePage.SearchComponent.ClickSubmitAsync();
    }
}