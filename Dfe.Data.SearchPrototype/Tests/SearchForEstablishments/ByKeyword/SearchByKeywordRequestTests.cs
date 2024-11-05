using Dfe.Data.SearchPrototype.SearchForEstablishments.ByKeyword.Usecase;
using FluentAssertions;
using Xunit;

namespace Dfe.Data.SearchPrototype.Tests.SearchForEstablishments.ByKeyword;

public class SearchByKeywordRequestTests
{
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
        var request = new SearchByKeywordRequest("searchKeyword", filterList);

        // assert
        request.FilterRequests.Should().NotBeNull();
        foreach (var item in filterList)
        {
            var matchingRequest = request.FilterRequests!.First(x => x.FilterName == item.FilterName);
            matchingRequest.FilterValues.Should().BeEquivalentTo(item.FilterValues);
        }
    }

    [Fact]
    public void Constructor_WithNoFilterParam_HasFilterRequestsNull()
    {
        // act
        var request = new SearchByKeywordRequest("searchKeyword");

        // assert
        request.FilterRequests.Should().BeNull();
    }

    [Fact]
    public void Constructor_WithSetOffsetValue_AssignsCorrectPropertyValue()
    {
        //arrange
        const int Offset = 10;
        // act
        var request = new SearchByKeywordRequest("searchKeyword", Offset);

        // assert
        request.Offset.
            Should().Be(Offset);
    }

    [Fact]
    public void Constructor_WithDefaultOffsetValue_AssignsDefaultPropertyValue()
    {
        // act
        var request = new SearchByKeywordRequest("searchKeyword");

        // assert
        request.Offset.
            Should().Be(0);//value of zero ensures no records are skipped
    }
}
