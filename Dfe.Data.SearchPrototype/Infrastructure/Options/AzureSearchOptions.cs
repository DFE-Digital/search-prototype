namespace Dfe.Data.SearchPrototype.Infrastructure.Options;

public class AzureSearchOptions
{
    public string SearchIndex { get; set; }
    public int SearchMode { get; set; }
    public int Size { get; set; }
    public bool IncludeTotalCount { get; set; }
}
