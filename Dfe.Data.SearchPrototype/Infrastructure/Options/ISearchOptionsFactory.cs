using Azure.Search.Documents;

namespace Dfe.Data.SearchPrototype.Infrastructure.Options;

/// <summary>
/// Contract in support of concrete implementations defined in order to
/// return an "T:Azure.Search.Documents.SearchOptions" instance based
/// on the key target collection specified.
/// </summary>
public interface ISearchOptionsFactory
{
    /// <summary>
    /// Specifies the behavior for retrieving an
    /// "T:Dfe.Data.SearchPrototype.Infrastructure.Options.SearchSettingsOptions"
    /// instance by the specified target collection key.
    /// </summary>
    /// <param name="targetCollection">
    /// The key used to target the configuration block describing the collection under scrutiny.
    /// </param>
    /// <returns>
    /// A configured instance of "T:Azure.Search.Documents.SearchOptions".
    /// </returns>
    public SearchOptions? GetSearchOptions(string targetCollection);
}