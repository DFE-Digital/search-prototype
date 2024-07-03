using Azure.Search.Documents;

namespace Dfe.Data.SearchPrototype.Infrastructure.Options;

public interface ISearchOptionsFactory
{
    public SearchOptions GetSearchOptions(string targetCollection);
}