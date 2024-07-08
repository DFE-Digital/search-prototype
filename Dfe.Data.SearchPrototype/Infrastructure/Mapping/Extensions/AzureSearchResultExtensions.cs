using Azure.Search.Documents.Models;
using Newtonsoft.Json;
using System.Dynamic;

namespace Dfe.Data.SearchPrototype.Infrastructure.Mapping.Extensions
{
    /// <summary>
    /// Provides an extension over the azure search results allowing
    /// for the "P:Azure.Search.Documents.Models.Document" to be
    /// deserialised to a "T:System.Dynamic.ExpandoObject" instance.
    /// </summary>
    public static class AzureSearchResultExtensions
    {
        /// <summary>
        /// Takes the "T:Azure.Search.Documents.Models.SearchResult"
        /// instance and attempts to deserialis the "P:Azure.Search.Documents.Models.Document"
        /// to a usable "T:System.Dynamic.ExpandoObject".
        /// </summary>
        /// <param name="searchResult">
        /// The raw azure "T:Azure.Search.Documents.Models.SearchResult" instance.
        /// </param>
        /// <returns>
        /// An "T:System.Dynamic.ExpandoObject" instance of
        /// the translated search results document.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Exception type thrown if no search document can be derived from search results.
        /// </exception>
        /// <exception cref="JsonSerializationException">
        /// Exception thrown if there is a problem deserialising the derived string doument object.
        /// </exception>
        public static ExpandoObject DeserialiseSearchResultDocument(this SearchResult<object> searchResult)
        {
            ArgumentNullException.ThrowIfNull(searchResult);

            string? searchDocument = searchResult.Document!.ToString();

            if (string.IsNullOrWhiteSpace(searchDocument))
            {
                throw new ArgumentException("An empty or null search document cannot be serialised.");
            }

            var serialisedSearchResult =
                JsonConvert.DeserializeObject<ExpandoObject>(searchDocument!);

            return serialisedSearchResult ??
                throw new JsonSerializationException(
                    $"Unable to deserialise search result document: {searchDocument}");
        }
    }
}
