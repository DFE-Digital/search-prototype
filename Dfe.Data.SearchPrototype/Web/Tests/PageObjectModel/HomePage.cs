using AngleSharp.Html.Dom;
using Dfe.Data.SearchPrototype.Web.Tests.PageObjectModel.PageComponents;
using Dfe.Data.SearchPrototype.Web.Tests.PageObjectModel.Setup;
using Microsoft.AspNetCore.Mvc.Testing;
using OpenQA.Selenium;

namespace Dfe.Data.SearchPrototype.Web.Tests.PageObjectModel
{
    public sealed class HomePage : DocumentObjectModelExtractor
    {
        private readonly PageHeader _pageHeader;
        private readonly SearchComponent _searchComponent;

        private const string PageName = "Home";
        private const string PrivacyLink = "Privacy";
        private const string MainHeadingClass = "govuk-header__link govuk-header__service-name";

        public By Heading => By.CssSelector("header div div:nth-of-type(2) a");
        public By SearchHiddenDiv => By.CssSelector("#searchKeyWord + div");

        public HomePage(WebApplicationFactory<Program> webApplicationFactory) :
            base(webApplicationFactory, PageName)
        {
            ArgumentNullException.ThrowIfNull(DocumentObjectModel);
            _pageHeader = PageHeader.Create(DocumentObjectModel);
            _searchComponent = SearchComponent.Create(DocumentObjectModel);
        }

        public SearchComponent SearchComponent  => _searchComponent;

        public string GetHomePageHeading() =>
            _pageHeader.GetMainHeading(MainHeadingClass);

        public IHtmlAnchorElement GetHomePagePrivacyLink() =>
            _pageHeader.GetHeaderLink(PrivacyLink);

        public IHtmlInputElement GetSearchInputBox() =>
            _searchComponent.GetTextInputBox("input");

        public IHtmlButtonElement GetSearchSubmitButton() =>
            _searchComponent.GetSubmitButton("button");

        public static HomePage Create(
            WebApplicationFactory<Program> webApplicationFactory) => new(webApplicationFactory);
    }
}