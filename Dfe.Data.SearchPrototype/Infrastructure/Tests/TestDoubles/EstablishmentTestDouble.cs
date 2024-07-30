using Bogus;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;

public static class EstablishmentTestDouble
{
    public static Establishment Create()
    {
        var searchResultFaker =
           new Faker<Establishment>()
           .StrictMode(false)
              .RuleFor(
                   establishment => establishment.ESTABLISHMENTNAME,
                   _ => new Bogus.Faker().Company.CompanyName())
              .RuleFor(
                    establishment => establishment.id,
                    _ => new Bogus.Faker().Random.Number(100000, 999999).ToString())
              .RuleFor(
                    establishment => establishment.STREET,
                    _ => new Bogus.Faker().Address.StreetName())
              .RuleFor(
                    establishment => establishment.LOCALITY,
                    _ => new Bogus.Faker().Address.City())
              .RuleFor(
                    establishment => establishment.ADDRESS3,
                    _ => new Bogus.Faker().Address.City())
              .RuleFor(
                    establishment => establishment.TOWN,
                    _ => new Bogus.Faker().Address.City())
              .RuleFor(
                    establishment => establishment.POSTCODE,
                    _ => new Bogus.Faker().Address.ZipCode())
              .RuleFor(
                    establishment => establishment.TYPEOFESTABLISHMENTNAME,
                    _ => new Bogus.Faker().Random.Word());
        return searchResultFaker.Generate();
    }
}