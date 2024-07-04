using Azure.Search.Documents.Models;
using Newtonsoft.Json;
using System.Dynamic;

namespace Dfe.Data.SearchPrototype.Infrastructure.Mapping.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    internal static class AzureSearchResultExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchResult"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="JsonSerializationException"></exception>
        public static ExpandoObject DeserialiseSearchResultDocument(this SearchResult<object> searchResult)
        {
            ArgumentNullException.ThrowIfNull(searchResult);

            string? searchDocument = searchResult.Document.ToString();

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
