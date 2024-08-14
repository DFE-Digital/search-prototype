using Dfe.Data.SearchPrototype.Common.Mappers;
using Dfe.Data.SearchPrototype.Infrastructure.Mappers;
using Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;
using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;
using FluentAssertions;
using Xunit;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.Mappers;

public sealed class AzureSearchResultToEstablishmentMapperTests
{
    IMapper<Establishment, SearchForEstablishments.Models.Establishment> _establishmentMapper;
    IMapper<Establishment, Address> _addressMapper;

    public AzureSearchResultToEstablishmentMapperTests()
    {
        _addressMapper = new AzureSearchResultToAddressMapper();
        _establishmentMapper = new AzureSearchResultToEstablishmentMapper(_addressMapper);
    }

    [Fact]
    public void MapFrom_With_Valid_Search_Result_Returns_Configured_Establishment()
    {
        // arrange
        Establishment establishmentFake = EstablishmentTestDouble.Create();

        // act
        SearchForEstablishments.Models.Establishment? result = _establishmentMapper.MapFrom(establishmentFake);

        // assert
        result.Should().NotBeNull();
        result.Urn.Should().Be(establishmentFake.id);
        result.Name.Should().Be(establishmentFake.ESTABLISHMENTNAME);
        result.Address.Street.Should().Be(establishmentFake.STREET);
        result.Address.Locality.Should().Be(establishmentFake.LOCALITY);
        result.Address.Address3.Should().Be(establishmentFake.ADDRESS3);
        result.Address.Town.Should().Be(establishmentFake.TOWN);
        result.Address.Postcode.Should().Be(establishmentFake.POSTCODE);
        result.EstablishmentType.Should().Be(establishmentFake.TYPEOFESTABLISHMENTNAME);
        result.PhaseOfEducation.Should().Be(establishmentFake.PHASEOFEDUCATION);
        result.EstablishmentStatusName.Should().Be(establishmentFake.ESTABLISHMENTSTATUSNAME);
    }

    [Fact]
    public void MapFrom_With_Null_Search_Result_Throws_Expected_Argument_Null_Exception()
    {
        // act.
        Establishment establishmentFake = null!;

        // act.
        _establishmentMapper
            .Invoking(mapper =>
                mapper.MapFrom(establishmentFake))
                    .Should()
                        .Throw<NullReferenceException>();
    }

    [Fact]
    public void MapFrom_With_Null_id_Throws_Expected_Argument_Exception()
    {
        // arrange
        var establishmentFake = new Establishment()
        {
            id = null!,
            ESTABLISHMENTNAME = "Test Establishment",
            TYPEOFESTABLISHMENTNAME = "secondaryFake",
            ESTABLISHMENTSTATUSNAME = "fake status",
            PHASEOFEDUCATION = "fake primary"
        };

        // act, assert
        _establishmentMapper
            .Invoking(mapper =>
                mapper.MapFrom(establishmentFake))
                    .Should()
                        .Throw<ArgumentException>()
                        .WithMessage("Value cannot be null. (Parameter 'id')");
    }

    [Fact]
    public void MapFrom_With_Null_Name_Throws_Expected_Argument_Exception()
    {
        // arrange
        var establishmentFake = new Establishment()
        {
            id = "123456",
            TYPEOFESTABLISHMENTNAME = "secondaryFake",
            ESTABLISHMENTSTATUSNAME = "fake status",
            PHASEOFEDUCATION = "fake primary",
            ESTABLISHMENTNAME = null!
        };

        // act, assert
        _establishmentMapper
            .Invoking(mapper =>
                mapper.MapFrom(establishmentFake))
                    .Should()
                        .Throw<ArgumentException>()
                        .WithMessage("Value cannot be null. (Parameter 'ESTABLISHMENTNAME')");
    }

    [Fact]
    public void MapFrom_With_NullPhaseOfEducation_Throws_Expected_Argument_Exception()
    {
        // arrange
        var establishmentFake = new Establishment()
        {
            id = "123456",
            ESTABLISHMENTNAME = "Test Establishment",
            TYPEOFESTABLISHMENTNAME = "secondaryFake",
            ESTABLISHMENTSTATUSNAME = "fake status",
            PHASEOFEDUCATION = null!
        };

        // act, assert
        _establishmentMapper
            .Invoking(mapper =>
                mapper.MapFrom(establishmentFake))
                    .Should()
                        .Throw<ArgumentException>()
                        .WithMessage("Value cannot be null. (Parameter 'PHASEOFEDUCATION')");
    }

    [Fact]
    public void MapFrom_With_NullTypeOfEstablishment_Throws_Expected_Argument_Exception()
    {
        // arrange
        var establishmentFake = new Establishment()
        {
            id = "1111",
            ESTABLISHMENTNAME = "Test Establishment",
            PHASEOFEDUCATION = "primaryFake",
            ESTABLISHMENTSTATUSNAME = "closedFake",
            TYPEOFESTABLISHMENTNAME = null!
        };

        // act, assert
        _establishmentMapper
            .Invoking(mapper =>
                mapper.MapFrom(establishmentFake))
                    .Should()
                        .Throw<ArgumentException>()
                        .WithMessage("Value cannot be null. (Parameter 'TYPEOFESTABLISHMENTNAME')");
    }

    [Fact]
    public void MapFrom_With_NullEstablishmentStatus_Throws_Expected_Argument_Exception()
    {
        // arrange
        var establishmentFake = new Establishment()
        {
            id = "1111",
            ESTABLISHMENTNAME = "Test Establishment",
            PHASEOFEDUCATION = "primaryFake",
            TYPEOFESTABLISHMENTNAME = "fakeType",
            ESTABLISHMENTSTATUSNAME = null!
        };

        // act, assert
        _establishmentMapper
            .Invoking(mapper =>
                mapper.MapFrom(establishmentFake))
                    .Should()
                        .Throw<ArgumentException>()
                        .WithMessage("Value cannot be null. (Parameter 'ESTABLISHMENTSTATUSNAME')");
    }

    [Theory]
    [InlineData(null, "fakeLocality", "", "fakeTown", "FakePostCode")]
    [InlineData(null, "", null, "fakeTown", "FakePostCode")]
    [InlineData("fakeStreet", null, "", null, "FakePostCode")]
    [InlineData("", null, null, null, null)]
    public void MapFrom_With_NullAddressValues_Returns_Configured_Establishment(
        string street, string locality, string address3, string town, string postcode)
    {
        // arrange
        Establishment establishmentFake = new Establishment()
        {
            id = "000000",
            ESTABLISHMENTNAME = "fakename",
            TYPEOFESTABLISHMENTNAME = "FakeType",
            PHASEOFEDUCATION = "fakePhaseOfEducation",
            STREET = street,
            LOCALITY = locality,
            ADDRESS3 = address3,
            TOWN = town,
            POSTCODE = postcode,
            ESTABLISHMENTSTATUSNAME = "fakeStatus"
        };

        // act
        SearchForEstablishments.Models.Establishment? result = _establishmentMapper.MapFrom(establishmentFake);

        // assert
        result.Should().NotBeNull();
        result.Address.Street.Should().Be(establishmentFake.STREET);
        result.Address.Locality.Should().Be(establishmentFake.LOCALITY);
        result.Address.Address3.Should().Be(establishmentFake.ADDRESS3);
        result.Address.Town.Should().Be(establishmentFake.TOWN);
        result.Address.Postcode.Should().Be(establishmentFake.POSTCODE);

    }
}