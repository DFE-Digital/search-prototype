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
        var filterList = new List<KeyValuePair<string, IList<object>>>()
        {
            new KeyValuePair<string, IList<object>>("EducationPhase", new List<object>() {"Primary", "Secondary"}),
            new KeyValuePair<string, IList<object>>("MaybeATypeCode", new List<object>() {1,2})
        };

        // act
        var request = new SearchByKeywordRequest("searchKeyword", filterList);

        // assert
        request.FilterRequests.Should().NotBeNull();
        foreach (var item in filterList)
        {
            var matchingRequest = request.FilterRequests!.First(x => x.FilterName == item.Key);
            matchingRequest.FilterValues.Should().BeEquivalentTo(item.Value);
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
}
