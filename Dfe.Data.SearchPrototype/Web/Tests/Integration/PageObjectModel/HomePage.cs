using AngleSharp.Html.Dom;
using DfE.Data.SearchPrototype.Web.Tests.Integration.PageObjectModel.PageComponents;
using DfE.Data.SearchPrototype.Web.Tests.Integration.PageObjectModel.Setup;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DfE.Data.SearchPrototype.Web.Tests.Integration.PageObjectModel
{
    public sealed class HomePage : PageObjectModelExtractor
    {
        private readonly PageHeader _pageHeader;

        public HomePage(WebApplicationFactory<Program> webApplicationFactory) :
            base(webApplicationFactory)
        {
            _pageHeader =
                PageHeader.Create(webApplicationFactory, PageName);
        }

        public string GetHomePageHeading() =>
            _pageHeader.GetMainHeading("govuk-header__link govuk-header__service-name");

        public IHtmlAnchorElement GetHomePageHeaderLink() =>
            _pageHeader.GetHeaderLink(linkName: "Privacy");

        private const string PageName = "Home";

        public static HomePage Create(
            WebApplicationFactory<Program> webApplicationFactory) => new(webApplicationFactory);
    }
}