﻿using Dfe.Data.SearchPrototype.Infrastructure.Mappers;
using Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;
using FluentAssertions;
using Xunit;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.Mappers;

public sealed class AzureSearchResultToEstablishmentMapperTests
{
    IMapper<Establishment, SearchForEstablishments.Establishment> _establishmentMapper;
    IMapper<Establishment, SearchForEstablishments.Address> _addressMapper;

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
        SearchForEstablishments.Establishment? result = _establishmentMapper.MapFrom(establishmentFake);

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
        // arrange.
        const string EstablishmentName = "Test Establishment";

        // act.
        var establishmentFake = new Establishment()
        {
            id = null!,
            ESTABLISHMENTNAME = EstablishmentName
        };

        // act.
        _establishmentMapper
            .Invoking(mapper =>
                mapper.MapFrom(establishmentFake))
                    .Should()
                        .Throw<ArgumentException>();
    }

    [Fact]
    public void MapFrom_With_Null_Name_Throws_Expected_Argument_Exception()
    {
        // arrange.
        const string EstablishmentId = "123456";

        // act.
        var establishmentFake = new Establishment()
        {
            id = EstablishmentId,
            ESTABLISHMENTNAME = null!
        };

        // act.
        _establishmentMapper
            .Invoking(mapper =>
                mapper.MapFrom(establishmentFake))
                    .Should()
                        .Throw<ArgumentException>();
    }
}
