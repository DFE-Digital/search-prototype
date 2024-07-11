using Azure.Search.Documents;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;

namespace Dfe.Data.SearchPrototype.Infrastructure.Options.Mapping;

/// <summary>
/// Concrete mapper implementation which translates the results returned from the internal
/// "T:Dfe.Data.SearchPrototype.Infrastructure.Options.SearchSettingsOptions",
/// and maps them to the "T:Azure.Search.Documents.SearchOptions"
/// type required to configure and invoke the Azure search.
/// </summary>
public sealed class SearchOptionsToAzureOptionsMapper : IMapper<SearchSettingsOptions, SearchOptions>
{
    /// <summary>
    /// Entry point for mapping from the received
    /// "T:Dfe.Data.SearchPrototype.Infrastructure.Options.SearchSettingsOptions"
    /// to the returned "T:Azure.Search.Documents.SearchOptions" instance.
    /// </summary>
    /// <param name="input">
    /// The internally configured search settings which
    /// seed the "T:Azure.Search.Documents.SearchOptions" mapping.
    /// </param>
    /// <returns>
    /// A fully configured "T:Azure.Search.Documents.SearchOptions" instance.
    /// </returns>
    public SearchOptions MapFrom(SearchSettingsOptions input)
    {
        ArgumentNullException.ThrowIfNull(input);

        var searchOptions = new SearchOptions()
        {
            SearchMode = input.SearchMode,
            Size = input.Size,
            IncludeTotalCount = input.IncludeTotalCount
        };

        input.SearchFields?.ToList()
            .ForEach(searchfield =>
                searchOptions.SearchFields.Add(searchfield));

        input.SelectFields?.ToList()
            .ForEach(searchOptions.Select.Add);

        return searchOptions;
    }
}
