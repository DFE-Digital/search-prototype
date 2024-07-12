using DfE.Data.SearchPrototype.Web.Tests.Integration.PageObjectModel.Setup;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DfE.Data.SearchPrototype.Web.Tests.Integration.PageObjectModel
{
    public sealed class PrivacyPage : DocumentObjectModelExtractor
    {
        private const string PageName = "Privacy";
        private const string TitleElement = "h1";

        public PrivacyPage(WebApplicationFactory<Program> webApplicationFactory, string pageName) :
            base(webApplicationFactory, pageName){
        }

        public string GetPrivacyPageTitle()
        {
            ArgumentNullException.ThrowIfNull(DocumentObjectModel);
            return DocumentObjectModel.GetElementsByTagName(TitleElement).Single().InnerHtml;
        }

        public static PrivacyPage NavigateToPage(
            WebApplicationFactory<Program> webApplicationFactory, string pageName) => new(webApplicationFactory, pageName);

        public static PrivacyPage Create(
            WebApplicationFactory<Program> webApplicationFactory) => new(webApplicationFactory, PageName);
    }
}