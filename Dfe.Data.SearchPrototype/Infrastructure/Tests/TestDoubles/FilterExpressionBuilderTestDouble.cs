using Dfe.Data.Common.Infrastructure.CognitiveSearch.Filtering;
using Moq;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;

public class FilterExpressionBuilderTestDouble
{
    private Mock<ISearchFilterExpressionsBuilder> _mock = new();
    private string? _response;
    private IEnumerable<SearchFilterRequest>? _searchFilterRequests;

    public FilterExpressionBuilderTestDouble WithResponse(string response)
    {
        _response = response;
        return this;
    }

    public FilterExpressionBuilderTestDouble ExpectingRequest(IEnumerable<SearchFilterRequest> filterRequests)
    {
        _searchFilterRequests = filterRequests;
        return this;
    }

    public ISearchFilterExpressionsBuilder Create()
    {
        var input = _searchFilterRequests ?? It.IsAny<IEnumerable<SearchFilterRequest>>();
        var response = _response ?? It.IsAny<string>();
        _mock.Setup(x => x.BuildSearchFilterExpressions(It.IsAny<IEnumerable<SearchFilterRequest>>()))
            .Returns(response)
            .Verifiable();
        return _mock.Object;
    }
}
