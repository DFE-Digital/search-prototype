﻿using Dfe.Data.SearchPrototype.SearchForEstablishments.ByKeyword.Usecase;

namespace Dfe.Data.SearchPrototype.SearchForEstablishments.ByKeyword.ServiceAdapters;

/// <summary>
/// Prescribes the context of the search including
/// the keyword, search fields, and facets to use.
/// </summary>
public sealed class SearchServiceAdapterRequest
{
    /// <summary>
    /// The search keyword(s) to be applied.
    /// </summary>
    public string SearchKeyword { get; }

    /// <summary>
    /// The collection of fields in the underlying collection to search over.
    /// </summary>
    public IList<string> SearchFields { get; }

    /// <summary>
    /// The collection of facets to apply in the search request.
    /// </summary>
    public IList<string> Facets { get; }

    /// <summary>
    /// The dictionary of filter requests where the key is the name of the filter and the value is the list of filter values.
    /// </summary>
    public IList<FilterRequest>? SearchFilterRequests { get; }

    /// <summary>
    /// The following arguments are passed via the constructor and are not changeable
    /// once an instance is created, this ensures we preserve immutability.
    /// </summary>
    /// <param name="searchKeyword">
    /// The search keyword(s) to be applied.
    /// </param>
    /// <param name="searchFields">
    /// The collection of fields in the underlying collection to search over.
    /// </param>
    /// <param name="facets">
    /// The collection of facets to apply in the search request.
    /// </param>
    /// <param name="searchFilterRequests">
    /// Dictionary of search filter requests where key is the name of the filter and the value is the list of filter values.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The exception thrown if an invalid search keyword (null or whitespace) is prescribed.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// The exception type thrown if either a null or empty collection of search fields,
    /// or search facets are prescribed.
    /// </exception>
    public SearchServiceAdapterRequest(string searchKeyword, IList<string> searchFields, IList<string> facets, IList<FilterRequest>? searchFilterRequests = null)
    {
        SearchKeyword =
            string.IsNullOrWhiteSpace(searchKeyword) ?
                throw new ArgumentNullException(nameof(searchKeyword)) : searchKeyword;

        SearchFields = searchFields == null || searchFields.Count <= 0 ?
            throw new ArgumentException("", nameof(searchFields)) : searchFields;

        Facets = facets == null || facets.Count <= 0 ?
            throw new ArgumentException("", nameof(facets)) : facets;

        SearchFilterRequests = searchFilterRequests;
    }

    /// <summary>
    /// Factory method to allow implicit creation of a T:Dfe.Data.SearchPrototype.Search.SearchContext instance.
    /// </summary>
    /// <param name="searchKeyword">
    /// The keyword string which defines the search.
    /// </param>
    /// <param name="searchFields">
    /// The collection of fields in the underlying collection to search over.
    /// </param>
    /// <param name="facets">
    /// The collection of facets to apply in the search request.
    /// </param>
    /// <returns>
    /// A configured <see cref="SearchServiceAdapterRequest"/> instance.
    /// </returns>
    public static SearchServiceAdapterRequest Create(
        string searchKeyword, IList<string> searchFields, IList<string> facets) =>
            new(searchKeyword, searchFields, facets);
}
