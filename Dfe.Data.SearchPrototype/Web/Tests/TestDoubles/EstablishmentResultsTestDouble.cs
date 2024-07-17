using Dfe.Data.SearchPrototype.Search;

namespace Dfe.Data.SearchPrototype.Web.Tests.TestDoubles;

public static class EstablishmentResultsTestDouble
{
    private static string GetEstablishmentNameFake() =>
             new Bogus.Faker().Company.CompanyName();

    private static string GetEstablishmentIdentifierFake() =>
        new Bogus.Faker().Random.Int(100000,999999).ToString();

    public static EstablishmentResults Create()
    {
        var establishmentResults = new EstablishmentResults();

        for (int i = 0; i < new Bogus.Faker().Random.Int(1, 10); i++)
        {
            establishmentResults.AddEstablishment(new Establishment(GetEstablishmentIdentifierFake(), GetEstablishmentNameFake()));
        }
        return establishmentResults;
    }

    public static EstablishmentResults CreateWithNoResults()
    {
        return new EstablishmentResults();
    }
}
