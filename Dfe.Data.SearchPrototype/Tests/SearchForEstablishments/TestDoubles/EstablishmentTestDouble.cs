using Bogus;
using Dfe.Data.SearchPrototype.SearchForEstablishments;
using System.IO;

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

    private static string GetEstablishmentTypeFake() =>
        new Faker().Random.Word();

    private static StatusCode GetEstablishmentStatusCodeFake() =>
       (StatusCode)new Faker().Random.Int(0, 2);

    private static string GetEstablishmentEducationPhaseFake() =>
       new Faker().Random.Int(0, 1).ToString();

    public static Establishment Create()
    {
        var address = new Address()
        {
            Street = GetEstablishmentStreetFake(),
            Locality = GetEstablishmentLocalityFake(),
            Address3 = GetEstablishmentAddress3Fake(),
            Town = GetEstablishmentTownFake(),
            Postcode = GetEstablishmentPostcodeFake()
        };

        var educationPhase = new EducationPhase(
           isPrimary: GetEstablishmentEducationPhaseFake(),
           isSecondary: GetEstablishmentEducationPhaseFake(),
           isPost16: GetEstablishmentEducationPhaseFake());

        return new(
            urn: GetEstablishmentIdentifierFake(),
            name: GetEstablishmentNameFake(),
            address: address,
            establishmentType: GetEstablishmentTypeFake(),
            educationPhase: educationPhase,
            establishmentStatusCode: GetEstablishmentStatusCodeFake()
            );
    }
}
