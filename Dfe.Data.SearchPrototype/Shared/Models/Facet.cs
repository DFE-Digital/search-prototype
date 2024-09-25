using Dfe.Data.SearchPrototype.SearchForEstablishments.ByKeyword.Usecase;

namespace Dfe.Data.SearchPrototype.Shared.Models;

/// <summary>
/// Encapsulates the Faceted results returned by the <see cref="SearchByKeywordUseCase"/> instance.
/// </summary>
public class Facet
{
    /// <summary>
    /// The facet (field) name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The collection of <see cref="FacetValue"/> instances.
    /// </summary>
    public IList<FacetValue> Results { get; }

    /// <summary>
    ///  Establishes an immutable <see cref="Facet"/> instance via the constructor arguments specified.
    /// </summary>
    /// <param name="facetName">
    /// The name of the facet on which to assign the prescribed results.
    /// </param>
    /// <param name="facetResults">
    /// The collection of <see cref="FacetValue"/> instances that carry
    /// the facet values and count of matched items found.
    /// </param>
    public Facet(string facetName, IList<FacetValue> facetResults)
    {
        Name = facetName;
        Results = facetResults;
    }
}
