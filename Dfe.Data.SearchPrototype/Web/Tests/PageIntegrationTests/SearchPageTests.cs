using AngleSharp.Html.Dom;
using Dfe.Data.SearchPrototype.Web.Tests.PageObjectModel;
using Dfe.Data.SearchPrototype.Web.Tests.Shared.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Dfe.Data.SearchPrototype.Web.Tests.Integration
{
    public class SearchPageTests : IClassFixture<PageWebApplicationFactory>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public SearchPageTests(PageWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Search()
        {
            var response = await _factory.CreateClient().GetAsync("http://localhost:5000");

            var document = await HtmlHelpers.GetDocumentAsync(response);

            document.GetElementText(SearchPage.Heading.Criteria).Should().Be("Search prototype");
        }
    }
}
