using Bogus;
using Dfe.Data.SearchPrototype.Search;

namespace Dfe.Data.SearchPrototype.Tests.SearchForEstablishments.TestDoubles;

public class EstablishmentTestDouble
{
    private static string GetEstablishmentNameFake() =>
             new Faker().Company.CompanyName();

    private static string GetEstablishmentIdentifierFake() =>
        new Faker().Random.Int(100000, 999999).ToString();

    private static string GetEstablishmentStreetFake() =>
        new Faker().Address.StreetName();

    private static string GetEstablishmentLocalityFake() =>
        new Faker().Address.City();

    private static string GetEstablishmentAddress3Fake() =>
        new Faker().Address.City();

    private static string GetEstablishmentTownFake() =>
    new Faker().Address.City();

    private static string GetEstablishmentPostcodeFake() =>
        new Faker().Address.ZipCode();

    public static Establishment Create()
    {
        return new(
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
