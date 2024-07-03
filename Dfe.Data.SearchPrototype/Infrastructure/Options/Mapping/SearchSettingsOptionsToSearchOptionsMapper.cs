using DfE.Data.Services.CognitiveSearch.Search.Application.Options;

namespace Dfe.Data.SearchPrototype.Infrastructure.Options.Mapping;

public static class SearchSettingsOptionsToSearchOptionsMapper
{
    public static SearchOptions MapToSearchOptions(this SearchSettingsOptions searchSettingsOptions)
    {
        var searchOptions = new SearchOptions()
        {
            SearchMode = searchSettingsOptions.SearchMode,
            Size = searchSettingsOptions.Size,
            IncludeTotalCount = searchSettingsOptions.IncludeTotalCount
        };
        searchSettingsOptions.SearchFields?.ToList().ForEach(searchfield => searchOptions.SearchFields.Add(searchfield));
        searchSettingsOptions.Facets?.ToList().ForEach(facet => searchOptions.Facets.Add(facet));

        return searchOptions;
    }
}
