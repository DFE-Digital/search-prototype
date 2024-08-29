﻿using Dfe.Data.SearchPrototype.Common.Mappers;
using Dfe.Data.SearchPrototype.SearchForEstablishments;
using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;
using Dfe.Data.SearchPrototype.Tests.SearchForEstablishments.TestDoubles;
using FluentAssertions;
using Xunit;

namespace Dfe.Data.SearchPrototype.Tests.SearchForEstablishments;

public sealed class ResultsToResponseMapperTests
{
    [Fact]
    public void MapFrom_ValidInput_ReturnsCorrectResponse()
    {
        // arrange.
        var input = SearchResultsTestDouble.Create();
        IMapper<SearchResults, SearchByKeywordResponse> mapper = new ResultsToResponseMapper();

        // act.
        SearchByKeywordResponse response =  mapper.MapFrom(input);

        //assert.
        response.Should().NotBeNull();
        response.Status.Should().Be(SearchResponseStatus.Success);
        response.EstablishmentResults!.Establishments.Should().HaveCountGreaterThanOrEqualTo(1);
        response.EstablishmentResults!.Establishments.First().Urn.Should().Be(input.Establishments!.Establishments.First().Urn);
        response.EstablishmentResults!.Establishments.First().Name.Should().Be(input.Establishments.Establishments.First().Name);
        response.EstablishmentFacetResults!.Facets.Should().HaveCountGreaterThanOrEqualTo(1);
        response.EstablishmentFacetResults!.Facets.First().Name.Should().Be(input.Facets!.Facets.First().Name);
    }

    [Fact]
    public void MapFrom_EmptyResults_ReturnsSuccessResponse()
    {
        // arrange.
        var input = SearchResultsTestDouble.CreateWithNoResults();
        IMapper<SearchResults, SearchByKeywordResponse> mapper = new ResultsToResponseMapper();

        // act.
        SearchByKeywordResponse response = mapper.MapFrom(input);

        //assert.
        response.Should().NotBeNull();
        response.Status.Should().Be(SearchResponseStatus.Success);
        response.EstablishmentResults.Should().BeNull();
        response.EstablishmentFacetResults.Should().BeNull();
    }

    [Fact]
    public void MapFrom_NullInput_ReturnsErrorResponse()
    {
        // arrange.
        IMapper<SearchResults, SearchByKeywordResponse> mapper = new ResultsToResponseMapper();

        // act
        SearchByKeywordResponse response = mapper.MapFrom(null!);

        // assert
        response.Status.Should().Be(SearchResponseStatus.SearchServiceError);
    }
}
