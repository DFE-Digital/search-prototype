namespace Dfe.Data.SearchPrototype.SearchForEstablishments.Models;

public class SearchByKeywordCriteria
{
    public IList<string> SearchFields { get; set; }
    public IList<string> Facets { get; set; }
}
