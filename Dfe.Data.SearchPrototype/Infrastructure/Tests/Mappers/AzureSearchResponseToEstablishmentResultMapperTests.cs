using Azure;
using Azure.Search.Documents.Models;
using Dfe.Data.SearchPrototype.Infrastructure.Mappers;
using Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;
using Dfe.Data.SearchPrototype.Search;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;
using FluentAssertions;
using Xunit;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.Mappers;

public sealed class AzureSearchResponseToEstablishmentResultMapperTests
{
    [Fact]
    public void MapFrom_WithValidSearchResults_ReturnsConfiguredEstablishments()
    {
        // arrange
        IMapper<Establishment, Search.Establishment> azureSearchResultToEstablishmentMapper =
            new AzureSearchResultToEstablishmentMapper();

        IMapper<Response<SearchResults<Establishment>>, EstablishmentResults> mapper =
            new AzureSearchResponseToEstablishmentResultMapper(azureSearchResultToEstablishmentMapper);

        var searchResultDocuments =
            SearchResultFake.SearchResults();
        Response<SearchResults<Establishment>> responseFake =
            ResponseFake.WithSearchResults(searchResultDocuments);

        // act
        EstablishmentResults? result = mapper.MapFrom(responseFake);

        // assert
        result.Should().NotBeNull();
        result.Establishments.Should().HaveCount(searchResultDocuments.Count());
        foreach(var establishment in result.Establishments)
        {
            var matchingResult = searchResultDocuments.ToList().Find(x => establishment.Urn == x.Document.id);
            establishment.Name.Should().Be(matchingResult!.Document.ESTABLISHMENTNAME);
        }
    }

    [Fact]
    public void MapFrom_WithEmptySearchResults_ReturnsEmptyList()
    {
        // arrange
        IMapper<Establishment, Search.Establishment> azureSearchResultToEstablishmentMapper =
            new AzureSearchResultToEstablishmentMapper();

        IMapper<Response<SearchResults<Establishment>>, EstablishmentResults> mapper =
            new AzureSearchResponseToEstablishmentResultMapper(azureSearchResultToEstablishmentMapper);

        Response<SearchResults<Establishment>> responseFake =
            ResponseFake.WithSearchResults(SearchResultFake.EmptySearchResult());

        // act
        EstablishmentResults? result = mapper.MapFrom(responseFake);

        // assert
        result.Should().NotBeNull();
        result.Establishments.Should().HaveCount(0);
    }

    [Fact]
    public void MapFrom_WithNullSearchResults_ThrowsArgumentNullException()
    {
        // arrange
        IMapper<Establishment, Search.Establishment> azureSearchResultToEstablishmentMapper =
            new AzureSearchResultToEstablishmentMapper();

        IMapper<Response<SearchResults<Establishment>>, EstablishmentResults> mapper =
            new AzureSearchResponseToEstablishmentResultMapper(azureSearchResultToEstablishmentMapper);

        Response<SearchResults<Establishment>> responseFake = null!;

        // act.
        mapper
            .Invoking(mapper =>
                mapper.MapFrom(responseFake))
                    .Should()
                        .Throw<ArgumentNullException>()
                        .WithMessage("Value cannot be null. (Parameter 'input')");
    }

    [Fact]
    public void MapFrom_WithNullSearchResult_ThrowsInvalidOperationException()
    {
        // arrange
        IMapper<Establishment, Search.Establishment> azureSearchResultToEstablishmentMapper =
            new AzureSearchResultToEstablishmentMapper();

        IMapper<Response<SearchResults<Establishment>>, EstablishmentResults> mapper =
            new AzureSearchResponseToEstablishmentResultMapper(azureSearchResultToEstablishmentMapper);

        var searchResultDocuments =
            SearchResultFake.SearchResults();
        searchResultDocuments.Add(SearchResultFake.SearchResultWithDocument(null));
        Response<SearchResults<Establishment>> responseFake =
                    ResponseFake.WithSearchResults(searchResultDocuments);

        // act.
        mapper
            .Invoking(mapper =>
                mapper.MapFrom(responseFake))
                    .Should()
                        .Throw<InvalidOperationException>()
                        .WithMessage("Search result document object cannot be null.");
    }
}