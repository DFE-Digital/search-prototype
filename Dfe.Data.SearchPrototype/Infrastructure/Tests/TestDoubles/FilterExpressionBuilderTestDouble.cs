﻿using Dfe.Data.Common.Infrastructure.CognitiveSearch.Filtering;
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
        return Mock.Of<ISearchFilterExpressionsBuilder>();
    }
}
