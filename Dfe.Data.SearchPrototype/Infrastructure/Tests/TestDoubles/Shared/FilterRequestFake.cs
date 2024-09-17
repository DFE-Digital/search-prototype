using Dfe.Data.SearchPrototype.SearchForEstablishments.ByKeyword.Usecase;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles.Shared;

public static class FilterRequestFake
{
    public static FilterRequest Create()
    {
        return new FilterRequest("FilterName", new List<object>() { "value1", "value2" });
    }
}
