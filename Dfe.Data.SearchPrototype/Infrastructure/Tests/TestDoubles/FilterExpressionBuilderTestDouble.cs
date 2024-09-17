using Dfe.Data.Common.Infrastructure.CognitiveSearch.Filtering;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;

public static class FilterExpressionBuilderTestDouble
{
    public static ISearchFilterExpressionsBuilder Create()
    {
        var mock = new Mock<ISearchFilterExpressionsBuilder>();
        mock.Setup(x => x.BuildSearchFilterExpressions(It.IsAny<IEnumerable<SearchFilterRequest>>()))
            .Returns("filter expression string")
            .Verifiable();
        return mock.Object;
    }
}
