using Azure.Search.Documents.Models;

namespace Dfe.Data.SearchPrototype.Infrastructure.Options;

public class SearchSettingsOptions
{
    public string? SearchIndex { get; set; }
    public SearchMode SearchMode { get; set; }
    public int Size { get; set; }
    public bool IncludeTotalCount { get; set; }
    public IList<string>? SearchFields { get; set; }
    public IList<string>? Facets { get; set; }
}