using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;

public static class EstablishmentTestDouble
{
    private static string GetEstablishmentNameFake() =>
          new Bogus.Faker().Company.CompanyName();

    private static string GetEstablishmentIdentifierFake() =>
        new Bogus.Faker().Random.Int(100000, 999999).ToString();

    private static string GetEstablishmentStreetFake() =>
        new Bogus.Faker().Address.StreetName();

    private static string GetEstablishmentLocalityFake() =>
        new Bogus.Faker().Address.City();

    private static string GetEstablishmentAddress3Fake() =>
        new Bogus.Faker().Address.City();

    private static string GetEstablishmentTownFake() =>
    new Bogus.Faker().Address.City();

    private static string GetEstablishmentPostcodeFake() =>
        new Bogus.Faker().Address.ZipCode();

    private static string GetEstablishmentTypeFake() =>
        new Bogus.Faker().Random.Word();

    private static string GetEstablishmentEducationPhaseFake() =>
       new Bogus.Faker().Random.Word();

    private static string GetEstablishmentStatusCodeFake() =>
        new Bogus.Faker().Random.Int(0, 1).ToString();

    public static Establishment Create()
    {

        return new Establishment()
        {
            id = GetEstablishmentIdentifierFake(),
            ESTABLISHMENTNAME = GetEstablishmentNameFake(),
            STREET = GetEstablishmentStreetFake(),
            LOCALITY = GetEstablishmentLocalityFake(),
            ADDRESS3 = GetEstablishmentAddress3Fake(),
            TOWN = GetEstablishmentTownFake(),
            POSTCODE = GetEstablishmentPostcodeFake(),
            TYPEOFESTABLISHMENTNAME = GetEstablishmentTypeFake(),
            ESTABLISHMENTSTATUSCODE = GetEstablishmentStatusCodeFake(),
            PHASEOFEDUCATION = GetEstablishmentEducationPhaseFake()
        };
    }

    public static Establishment CreateWithStatusCode(string status)
    {
        var searchResultFake = Create();
        searchResultFake.ESTABLISHMENTSTATUSCODE = status;
        return searchResultFake;
    }

}
