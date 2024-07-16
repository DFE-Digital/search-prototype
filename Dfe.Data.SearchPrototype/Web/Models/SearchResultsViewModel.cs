namespace Dfe.Data.SearchPrototype.Web.Models
{
    public static class SearchResultsViewModel
    {
        private static List<SearchItemViewModel> searchItems = new List<SearchItemViewModel>
    {
        new SearchItemViewModel { Name = "Item1", URN = "Description1" },
        new SearchItemViewModel { Name = "Item2", URN = "Description2" }
    };

        public static List<SearchItemViewModel> GetItems(string searchKeyWord)
        {
            if (string.IsNullOrEmpty(searchKeyWord))
            {
                return searchItems;
            }

            return searchItems.Where(i => i.Name.Contains(searchKeyWord, StringComparison.OrdinalIgnoreCase) ||
                                     i.URN.Contains(searchKeyWord, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
}
