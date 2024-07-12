using Azure.Search.Documents.Models;

namespace Dfe.Data.SearchPrototype.Infrastructure.Options;

/// <summary>
/// Configuration options used to define the internal Azure cognitive search.
/// </summary>
public class SearchSettingsOptions
{
    /// <summary>
    /// The search index name of the collection under scrutiny.
    /// </summary>
    public string? SearchIndex { get; set; }
    /// <summary>
    /// The search mode defines whether to use any, or all search terms provisioned.
    /// </summary>
    public SearchMode SearchMode { get; set; }
    /// <summary>
    /// The size of the allowable search response.
    /// </summary>
    public int Size { get; set; }
    /// <summary>
    /// Allows the count of the total number of search records
    /// retrieved to be returned alongside the response.
    /// </summary>
    public bool IncludeTotalCount { get; set; }
    /// <summary>
    /// Specifies the fields over which the search will be conducted.
    /// </summary>
    public IList<string>? SearchFields { get; set; }
    /// <summary>
    /// Specifies the fields which will be returned in the search response.
    /// </summary>
    public IList<string>? SelectFields { get; set; }
    /// <summary>
    /// Specifies the allowable facets/filters to include in the search response.
    /// </summary>
    public IList<string>? Facets { get; set; }
}