using Dfe.Data.SearchPrototype.Infrastructure.Options;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;

internal static class AzureSearchOptionsTestDouble
{
    public static AzureSearchOptions Stub() => new()
    {
        SearchMode = 0,
        Size = 100,
        IncludeTotalCount = true,
        SearchIndex = "establishments"
    };
}
