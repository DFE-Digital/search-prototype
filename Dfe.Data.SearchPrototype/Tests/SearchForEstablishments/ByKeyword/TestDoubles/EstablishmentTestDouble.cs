﻿using Bogus;
using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;

namespace Dfe.Data.SearchPrototype.Tests.SearchForEstablishments.ByKeyword.TestDoubles;

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

    private static string GetEstablishmentStatusNameFake() =>
       new Faker().Random.Word();

    private static string GetEstablishmentEducationPhaseFake() =>
       new Faker().Random.Word();

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

        return new(
            urn: GetEstablishmentIdentifierFake(),
            name: GetEstablishmentNameFake(),
            address: address,
            establishmentType: GetEstablishmentTypeFake(),
            phaseOfEducation: GetEstablishmentEducationPhaseFake(),
            establishmentStatusName: GetEstablishmentStatusNameFake()
            );
    }
}
