using Dfe.Data.SearchPrototype.SearchForEstablishments.ByKeyword.ServiceAdapters;
using Dfe.Data.SearchPrototype.SearchForEstablishments.ByKeyword.Usecase;
using FluentAssertions;
using Xunit;

namespace Dfe.Data.SearchPrototype.Tests.SearchForEstablishments.ByKeyword.ServiceAdapters;

public class SearchServiceAdapterRequestTests
{
    [Fact]
    public void Constructor_WithNoFilterParam_AssignsCorrectPropertyValues()
    {
        // act
        var request = new SearchServiceAdapterRequest(
            searchKeyword:"searchKeyword",
            searchFields:["ESTABLISHMENTNAME", "TOWN"],
            facets:["facet1", "facet2"],
            offset: 10
            );

        // assert
        request.SearchKeyword.Should().Be("searchKeyword");
        request.SearchFields[0].Should().Be("ESTABLISHMENTNAME");
        request.SearchFields.Count().Should().Be(2);
        request.Facets.Contains("facet2");
        request.Facets.Should().HaveCount(2);
        request.Offset.Should().Be(10);
        request.SearchFilterRequests.Should().BeNull();
    }

    [Fact]
    public void Constructor_WithFilterParam_PopulatesFilterRequests()
    {
        // arrange
        var filterList = new List<FilterRequest>()
        {
            new FilterRequest("EducationPhase", new List<object>() {"Primary", "Secondary"}),
            new FilterRequest("MaybeATypeCode", new List<object>() {1,2})
        };

        // act
        var request = new SearchServiceAdapterRequest(
            searchKeyword :"searchKeyword",
            searchFields: ["ESTABLISHMENTNAME", "TOWN"],
            facets: ["facet1", "facet2"],
            searchFilterRequests: filterList
            );

        // assert
        request.SearchFilterRequests.Should().NotBeNull();
        foreach (var item in filterList)
        {
            var matchingRequest = request.SearchFilterRequests!.First(x => x.FilterName == item.FilterName);
            matchingRequest.FilterValues.Should().BeEquivalentTo(item.FilterValues);
        }
    }

    [Fact]
    public void Constructor_WithNullSearchKeyword_ThrowsArgumentNullException()
    {
        // act
        Action request =() => new SearchServiceAdapterRequest(
            searchKeyword: "",
            searchFields : ["ESTABLISHMENTNAME", "TOWN"],
            facets : ["facet1", "facet2"],
            offset: 10
            );

        // assert
        request.Should().Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'searchKeyword')")
            .And.ParamName.Should().Be("searchKeyword");
    }

    [Fact]
    public void Constructor_WithNullSearchFields_ThrowsArgumentException()
    {
        // act
        Action request = () => 
            new SearchServiceAdapterRequest(
                searchKeyword: "searchKeyword",
                searchFields: null!,
                facets: ["facet1", "facet2"],
                offset: 10
                );

        // assert
        request.Should().Throw<ArgumentException>()
            .WithMessage("A valid searchFields argument must be provided.");
    }

    [Fact]
    public void Constructor_WithNullFacets_ThrowsArgumentException()
    {
        // act
        Action request = () =>
            new SearchServiceAdapterRequest(
                searchKeyword:"searchKeyword",
                searchFields: ["ESTABLISHMENTNAME", "TOWN"],
                facets: null!,
                offset: 10
                );

        // assert
        request.Should().Throw<ArgumentException>()
            .WithMessage("A valid facets argument must be provided.");
    }
}
