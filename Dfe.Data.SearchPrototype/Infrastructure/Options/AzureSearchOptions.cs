using System.ComponentModel.DataAnnotations;

namespace Dfe.Data.SearchPrototype.Infrastructure.Options;

/// <summary>
/// The search options to use by the <see cref="CognitiveSearchServiceAdapter{TSearchResult}"/>
/// which is set using the IOptions interface
/// </summary>
public class AzureSearchOptions
{
    /// <summary>
    /// The Azure AI Search index used to target for search requests.
    /// </summary>
    [Required]
    public string SearchIndex { get; set; } = string.Empty;
    /// <summary>
    /// The Azure Search mode <a href="https://learn.microsoft.com/en-us/dotnet/api/azure.search.documents.models.searchmode?view=azure-dotnet">
    /// see documentation for details</a>
    /// </summary>
    [Range(0, 1, ErrorMessage = "Search Mode must be 0 (Any search terms may match) or 1 (All search terms must match) only.")]
    public int SearchMode { get; set; }
    /// <summary>
    /// The number of search results returned.
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Size must be greater than zero.")]
    public int Size { get; set; }
    /// <summary>
    /// The number of search results returned.
    /// </summary>
    public bool IncludeTotalCount { get; set; }
}
