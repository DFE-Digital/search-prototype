using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using Dfe.Data.Common.Infrastructure.CognitiveSearch.Filtering;
using Dfe.Data.SearchPrototype.Infrastructure.Builders;
using Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;
using Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles.Shared;
using Dfe.Data.SearchPrototype.SearchForEstablishments.ByKeyword.Usecase;
using FluentAssertions;
using Moq;
using Xunit;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.Builders
{
    public sealed class SearchOptionsBuilderTests
    {
        [Fact]
        public void Build_WithSize_SearchOptionsWithCorrectSize()
        {
            // arrange
            ISearchFilterExpressionsBuilder mockSearchFilterExpressionsBuilder = new FilterExpressionBuilderTestDouble().Create();

            ISearchOptionsBuilder searchOptionsBuilder = new SearchOptionsBuilder(mockSearchFilterExpressionsBuilder);

            // act
            SearchOptions searchOptions = searchOptionsBuilder.WithSize(size:100).Build();

            // assert
            searchOptions.Should().NotBeNull();
            searchOptions.Size.Should().Be(100);
        }

        [Fact]
        public void Build_WithSearchMode_SearchOptionsWithCorrectSearchMode()
        {
            // arrange
            ISearchFilterExpressionsBuilder mockSearchFilterExpressionsBuilder = new FilterExpressionBuilderTestDouble().Create();

            ISearchOptionsBuilder searchOptionsBuilder = new SearchOptionsBuilder(mockSearchFilterExpressionsBuilder);

            // act
            SearchOptions searchOptions = searchOptionsBuilder.WithSearchMode(searchMode: SearchMode.Any).Build();

            // assert
            searchOptions.Should().NotBeNull();
            searchOptions.SearchMode.Should().Be(SearchMode.Any);
        }

        [Fact]
        public void Build_WithIncludeTotalCount_SearchOptionsWithIncludeTotalCount()
        {
            // arrange
            ISearchFilterExpressionsBuilder mockSearchFilterExpressionsBuilder = new FilterExpressionBuilderTestDouble().Create();

            ISearchOptionsBuilder searchOptionsBuilder = new SearchOptionsBuilder(mockSearchFilterExpressionsBuilder);

            // act
            SearchOptions searchOptions = searchOptionsBuilder.WithIncludeTotalCount(includeTotalCount: true).Build();

            // assert
            searchOptions.Should().NotBeNull();
            searchOptions.IncludeTotalCount.Should().BeTrue();
        }

        [Fact]
        public void Build_WithSearchFields_SearchOptionsWithWithSearchFields()
        {
            // arrange
            ISearchFilterExpressionsBuilder mockSearchFilterExpressionsBuilder = new FilterExpressionBuilderTestDouble().Create();

            ISearchOptionsBuilder searchOptionsBuilder = new SearchOptionsBuilder(mockSearchFilterExpressionsBuilder);

            // act
            List<string> searchFields = ["FIELD_1", "FIELD_2", "FIELD_3"];

            SearchOptions searchOptions = searchOptionsBuilder.WithSearchFields(searchFields).Build();

            // assert
            searchOptions.Should().NotBeNull();
            searchOptions.SearchFields.Should().BeEquivalentTo(searchFields);
        }

        [Fact]
        public void Build_WithFacets_SearchOptionsWithWithFacets()
        {
            // arrange
            ISearchFilterExpressionsBuilder mockSearchFilterExpressionsBuilder = new FilterExpressionBuilderTestDouble().Create();

            ISearchOptionsBuilder searchOptionsBuilder = new SearchOptionsBuilder(mockSearchFilterExpressionsBuilder);

            // act
            List<string> searchFacets = ["FACET_1", "FACET_2", "FACET_3"];

            SearchOptions searchOptions = searchOptionsBuilder.WithFacets(searchFacets).Build();

            // assert
            searchOptions.Should().NotBeNull();
            searchOptions.Facets.Should().BeEquivalentTo(searchFacets);
        }

        [Fact]
        public void Build_WithFilters_CallsFilterBuilder_WithComposedFilterRequests()
        {
            // arrange
            var serviceAdapterInputFilterRequest =
                new List<FilterRequest>() { FilterRequestFake.Create(), FilterRequestFake.Create() };

            var mockSearchFilterExpressionsBuilder = new Mock<ISearchFilterExpressionsBuilder>();
            var requestMadeToFilterExpressionBuilder = new List<SearchFilterRequest>();

            mockSearchFilterExpressionsBuilder
                .Setup(builder => builder.BuildSearchFilterExpressions(It.IsAny<IEnumerable<SearchFilterRequest>>()))
                .Callback<IEnumerable<SearchFilterRequest>>((request) =>
                        requestMadeToFilterExpressionBuilder = request.ToList())
                .Returns("some filter string");

            ISearchOptionsBuilder searchOptionsBuilder =
                new SearchOptionsBuilder(mockSearchFilterExpressionsBuilder.Object);

            // act
            _ = searchOptionsBuilder.WithFilters(serviceAdapterInputFilterRequest).Build();

            // assert
            foreach (var filterRequest in serviceAdapterInputFilterRequest)
            {
                var matchingFilterRequest =
                    requestMadeToFilterExpressionBuilder
                        .First(request =>
                            request.FilterKey == filterRequest.FilterName);

                matchingFilterRequest.Should().NotBeNull();
                matchingFilterRequest.FilterValues.Should().BeEquivalentTo(filterRequest.FilterValues);
            }
        }
    }
}
