using Dfe.Data.SearchPrototype.Search;
using Dfe.Data.SearchPrototype.SearchForEstablishments;

namespace Dfe.Data.SearchPrototype.Web.Tests.Unit.TestDoubles;

public static class SearchByKeywordResponseTestDouble
{
    private static string GetEstablishmentNameFake() =>
             new Bogus.Faker().Company.CompanyName();

    private static string GetEstablishmentIdentifierFake() =>
        new Bogus.Faker().Random.Int(100000, 999999).ToString();

    public static SearchByKeywordResponse Create()
    {
        List<Establishment> establishmentResults = new();
        for (int i = 0; i < new Bogus.Faker().Random.Int(1, 10); i++)
        {
            establishmentResults.Add(new Establishment(GetEstablishmentIdentifierFake(), GetEstablishmentNameFake()));
        }
        return new SearchByKeywordResponse(establishmentResults);
       
    }

    public static SearchByKeywordResponse CreateWithNoResults()
    {
        return new SearchByKeywordResponse(null);
    }
}
