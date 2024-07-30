using Azure.Search.Documents;
using Dfe.Data.SearchPrototype.Common.Mappers;
using Microsoft.Extensions.Options;

namespace Dfe.Data.SearchPrototype.Infrastructure.Options;

/// <summary>
/// Concrete implementation for deriving "T:Dfe.Data.SearchPrototype.Infrastructure.Options.SearchSettingsOptions"
/// configuration snapshots and mapping them to the expected "T:Azure.Search.Documents.SearchOptions" instance.
/// </summary>
public class SearchOptionsFactory : ISearchOptionsFactory
{
    private readonly IOptionsSnapshot<SearchSettingsOptions> _searchSettingsOptions;
    private readonly IMapper<SearchSettingsOptions, SearchOptions> _searchOptionsToAzureOptionsMapper;

    /// <summary>
    /// Snapshot and concrete mapper implementation injected in order to support
    /// retrieval of the targeted configuration snapshot, and map to the expected
    /// "T:Azure.Search.Documents.SearchOptions" instance.
    /// </summary>
    /// <param name="searchSettingsOptions">
    /// The internally configured options.
    /// </param>
    /// <param name="searchOptionsToAzureOptionsMapper">
    /// The search options required to successfully perform an Azure search.
    /// </param>
    public SearchOptionsFactory(
        IOptionsSnapshot<SearchSettingsOptions> searchSettingsOptions,
        IMapper<SearchSettingsOptions, SearchOptions> searchOptionsToAzureOptionsMapper)
    {
        _searchSettingsOptions = searchSettingsOptions;
        _searchOptionsToAzureOptionsMapper = searchOptionsToAzureOptionsMapper;
    }

    /// <summary>
    /// Retrieves the "T:Dfe.Data.SearchPrototype.Infrastructure.Options.SearchSettingsOptions"
    /// instance (if configured) and attempts to map to "T:Azure.Search.Documents.SearchOptions" instance.
    /// </summary>
    /// <param name="targetCollection">
    /// The key used to target the configuration block describing the collection under scrutiny.
    /// </param>
    /// <returns>
    /// A configured instance of "T:Azure.Search.Documents.SearchOptions".
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Exception thrown if no configured target collection key is provided.
    /// </exception>
    public SearchOptions? GetSearchOptions(string targetCollection)
    {
        ArgumentNullException.ThrowIfNull(targetCollection);

        SearchSettingsOptions searchSettingsOptions =
            _searchSettingsOptions.Get(targetCollection);

        return (searchSettingsOptions == null) ?
            default : _searchOptionsToAzureOptionsMapper.MapFrom(searchSettingsOptions);
    }
}
