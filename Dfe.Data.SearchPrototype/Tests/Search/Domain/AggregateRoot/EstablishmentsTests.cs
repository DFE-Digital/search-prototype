using Dfe.Data.SearchPrototype.Search.Domain.AggregateRoot;
using Dfe.Data.SearchPrototype.Search.Domain.AggregateRoot.Entities;
using Dfe.Data.SearchPrototype.Search.Domain.AggregateRoot.ValueObjects;
using FluentAssertions;
using Xunit;

namespace Dfe.Data.SearchPrototype.Tests.Search.Domain.AggregateRoot
{
    public sealed class EstablishmentsTests
    {
        [Fact]
        public void Ctor_With_Valid_Identifier_Prescribed_Maintains_Prescribed_GUID_As_Identifier()
        {
            // arrange.
            Guid EstablishmentRootId = Guid.NewGuid();
            var establishmentsIdentifier = new EstablishmentsIdentifier(EstablishmentRootId);

            // act.
            var establsihments = new Establishments(establishmentsIdentifier);

            // assert.
            establsihments.Should().NotBeNull();
            establsihments.Identifier.Should().NotBeNull();
            establsihments.Identifier.EstablishmentsRootId.Should().Be(EstablishmentRootId.ToString());
        }

        [Fact]
        public void Create_Should_Make_New_Establishments_Insance_With_Valid_Identifier()
        {
            // act.
            var establsihments = Establishments.Create();

            // assert.
            establsihments.Should().NotBeNull();
            establsihments.Identifier.Should().NotBeNull();
            establsihments.Identifier.EstablishmentsRootId.Should().NotBeEmpty();
        }

        [Fact]
        public void Ctor_With_Null_Identifier_Throws_Expected_Argument_Null_Exception()
        {
            // act.
            Action constructor = () =>
                new Establishments(establishmentsIdentifier: null!);

            // assert.
            constructor.Should().Throw<ArgumentNullException>()
                .And.ParamName.Should().Be("identifier");
        }

        [Fact]
        public void AddEstablismentResult_With_Valid_Establishment_Assigns_Estabishment_To_Readonly_Collection()
        {
            // arrange.
            var establishments = Establishments.Create();
            var establishmentIdentifier = new EstablishmentIdentifier(urn: "123456");
            var establishmentDefinition = new EstablishmentDefinition(name: "St Johns RC Comprehensive");
            var establishment = new Establishment(establishmentIdentifier, establishmentDefinition);

            // act.
            establishments.AddEstablishment(establishment);

            // assert.
            establishments.EstablishmentCount.Should().Be(1);
            establishments.EstablishmentResults.Should().Contain(establishment);
        }

        [Fact]
        public void AddEstablismentResult_With_Null_Establishment_Throws_Expected_NullEstablishment_Exception()
        {
            // arrange.
            var establishments = Establishments.Create();

            // act.
            establishments
                 .Invoking(establishments =>
                    establishments.AddEstablishment(establishment: null!))
                        .Should()
                            .Throw<NullEstablishmentException>()
                            .WithMessage("Only configured establishment instances can be added to the read-only collection.");
        }
    }
}
