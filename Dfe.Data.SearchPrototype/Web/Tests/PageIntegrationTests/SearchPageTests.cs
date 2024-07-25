﻿using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Dfe.Data.SearchPrototype.Web.Tests.PageObjectModel;
using Dfe.Data.SearchPrototype.Web.Tests.Shared.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Abstractions;

namespace Dfe.Data.SearchPrototype.Web.Tests.Integration
{
    public class SearchPageTests : IClassFixture<PageWebApplicationFactory>
    {
        private const string uri = "http://localhost:5000";
        private readonly HttpClient _client; 
        private readonly ITestOutputHelper _logger;
        private readonly WebApplicationFactory<Program> _factory;

        public SearchPageTests(PageWebApplicationFactory factory, ITestOutputHelper logger)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true
            });
            _logger = logger;
        }

        [Fact]
        public async Task Search_Title_IsDisplayed()
        {
            var response = await _factory.CreateClient().GetAsync(uri);

            var document = await HtmlHelpers.GetDocumentAsync(response);

            document.GetElementText(SearchPage.Heading.Criteria).Should().Be("Search prototype");
        }

        [Fact]
        public async Task Search_ByName_ReturnsResults()
        {
            var response = await _factory.CreateClient().GetAsync(uri);
            var document = await HtmlHelpers.GetDocumentAsync(response);

            var formElement = document.QuerySelector<IHtmlFormElement>(SearchPage.SearchForm.Criteria) ?? throw new Exception("Unable to find the sign in form");
            var formButton = document.QuerySelector<IHtmlButtonElement>(SearchPage.SearchButton.Criteria) ?? throw new Exception("Unable to find the submit button on search form");
            var searchTerm = "academy";

            var formResponse = await _client.SendAsync(
                formElement,
                formButton,
                new Dictionary<string, string>
                {
                    ["searchKeyWord"] = searchTerm
                });

            _logger.WriteLine("SendAsync client base address: " + _client.BaseAddress);
            _logger.WriteLine("SendAsync request message: " + formResponse.RequestMessage.ToString());

            var resultsPage = await HtmlHelpers.GetDocumentAsync(formResponse);

            _logger.WriteLine("Document: " + resultsPage.Body.OuterHtml);

            resultsPage.GetElementText(SearchPage.SearchResultsNumber.Criteria).Should().Contain("Results");
            resultsPage.GetMultipleElements(SearchPage.SearchResultLinks.Criteria).Count().Should().BeGreaterThan(1);
        }
    }
}
