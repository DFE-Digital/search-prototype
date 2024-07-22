namespace Dfe.Data.SearchPrototype.Web.Models;

public class SearchResultsViewModel
{
    public List<SearchItemViewModel>? SearchItems { get; set; }

    public bool HasResults => SearchResultsCount >= 1;
    public bool HasMoreThanOneResult => SearchResultsCount > 1;
    public int SearchResultsCount => SearchItems == null ? 0 : SearchItems.Count;
 }
