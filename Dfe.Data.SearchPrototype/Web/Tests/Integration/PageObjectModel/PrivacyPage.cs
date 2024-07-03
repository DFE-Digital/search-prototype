using DfE.Data.SearchPrototype.Web.Tests.Integration.PageObjectModel.PageComponents;
using DfE.Data.SearchPrototype.Web.Tests.Integration.PageObjectModel.Setup;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DfE.Data.SearchPrototype.Web.Tests.Integration.PageObjectModel
{
    public sealed class PrivacyPage : DocumentObjectModelExtractor
    {
        private readonly PageHeader _pageHeader;

        private const string PageName = "Privacy";
        private const string TitleElement = "h1";
        private const string MainHeadingClass = "govuk-header__link govuk-header__service-name";

        public PrivacyPage(WebApplicationFactory<Program> webApplicationFactory, string pageName) :
            base(webApplicationFactory, pageName)
        {
            _pageHeader = PageHeader.Create(DocumentObjectModel);
        }

        public string GetPrivacyPageHeading() =>
            _pageHeader.GetMainHeading(MainHeadingClass);

        public string GetPrivacyPageTitle() =>
            DocumentObjectModel.GetElementsByTagName(TitleElement).Single().InnerHtml;

        public static PrivacyPage NavigateToPage(
            WebApplicationFactory<Program> webApplicationFactory, string pageName) => new(webApplicationFactory, pageName);

        public static PrivacyPage Create(
            WebApplicationFactory<Program> webApplicationFactory) => new(webApplicationFactory, PageName);
    }
}