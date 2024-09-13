namespace Dfe.Data.SearchPrototype.SearchForEstablishments.ByKeyword.Usecase;

/// <summary>
/// Encapsulates a single filter to be applied to the search request
/// </summary>
public class FilterRequest
{
    /// <summary>
    /// The name of the filter field
    /// </summary>
    public string FilterName { get; }

    /// <summary>
    /// The readonly list of values of the filter to be included in the filter expression
    /// </summary>
    public IList<object> FilterValues  => _filterValues.AsReadOnly();

    private IList<object> _filterValues;

    /// <summary>
    /// Constructor that initialises the <see cref="FilterName"/> and the <see cref="FilterValues"/>
    /// </summary>
    /// <param name="filterName"></param>
    /// <param name="filterValues"></param>
    public FilterRequest(string filterName, IList<object> filterValues)
    {
        FilterName = filterName;
        _filterValues = filterValues;
    }
}
