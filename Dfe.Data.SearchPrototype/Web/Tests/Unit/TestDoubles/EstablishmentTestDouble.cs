using Bogus;
using Dfe.Data.SearchPrototype.Search;

namespace Dfe.Data.SearchPrototype.Web.Tests.Unit.TestDoubles;

public class EstablishmentTestDouble
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

    public static Establishment Create()
    {
        return new (
            urn: GetEstablishmentIdentifierFake(),
            name: GetEstablishmentNameFake(),
            street: GetEstablishmentStreetFake(),
            locality: GetEstablishmentLocalityFake(),
            address3: GetEstablishmentAddress3Fake(),
            town: GetEstablishmentTownFake(),
            postcode: GetEstablishmentPostcodeFake()
            );
    }
}

