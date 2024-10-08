﻿using Dfe.Data.SearchPrototype.Common.Mappers;
using Dfe.Data.SearchPrototype.Infrastructure.Mappers;
using Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;
using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;
using FluentAssertions;
using Xunit;

using AzureFacetResult = Azure.Search.Documents.Models.FacetResult;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.Mappers;

public sealed class AzureFacetResultToEstablishmentFacetsMapperTests
{
    AzureFacetResultToEstablishmentFacetsMapper _facetResultToFacetMapper = new();

    [Fact]
    public void MapFrom_WithStringFacetResults_ReturnsFacets()
    {
        // arrange
        var azureFacetsResults = new FacetsResultsFakeBuilder()
            .WithEducationPhaseFacet()
            .WithAutoGeneratedFacet()
            .Create();

        // act
        var mappedResult = _facetResultToFacetMapper.MapFrom(azureFacetsResults);

        // assert
        mappedResult.Should().NotBeNull();
        mappedResult.Facets.Should().NotBeNullOrEmpty();
        foreach (var azureFacet in azureFacetsResults)
        {
            mappedResult.Facets.First(facet => facet.Name == azureFacet.Key).Results.Should().NotBeNullOrEmpty();
            foreach(var expectedFacet in azureFacetsResults)
            {
                var mappedFacet = mappedResult.Facets.Single(facet => facet.Name == expectedFacet.Key);
                mappedFacet.Should().NotBeNull();
                foreach(var expectedFacetValue in expectedFacet.Value)
                {
                    var mappedfacetValue = mappedFacet.Results.Single(facetValue => facetValue.Value == expectedFacetValue.Value.ToString());
                    mappedFacet.Should().NotBeNull();
                    mappedfacetValue.Count.Should().Be(expectedFacetValue.Count);
                }
            }
        }
    }

    [Fact]
    public void MapFrom_WithNonStringFacetResults_ThrowsInvalidCastException()
    {
        // arrange
        var azureFacetsResults = new FacetsResultsFakeBuilder()
            .WithFacet(new List<object>() { true, "string2"})
            .Create();

        // act, assert
        Action failedAction = 
            () => _facetResultToFacetMapper.MapFrom(azureFacetsResults);

        InvalidCastException exception = Assert.Throws<InvalidCastException>(failedAction);

        exception.Message.Should().Be("Unable to cast object of type 'System.Boolean' to type 'System.String'.");
    }
}
