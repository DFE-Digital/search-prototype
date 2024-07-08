using Azure.Search.Documents;
using Dfe.Data.SearchPrototype.Infrastructure.Options;
using Dfe.Data.SearchPrototype.Infrastructure.Tests.Options.TestDoubles;
using FluentAssertions;
using Xunit;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.Options
{
    public sealed class SearchOptionsFactoryTests
    {
        [Fact]
        public void MatchesStringToOptions_ReturnsOptions()
        {
            // arrange
            const string TargetCollection = "Establishment";
            var options = SearchSettingsOptionsTestDouble.MockFor().Value;
            var searchOptionsToAzureOptionsMapperTestDoubles =
                SearchOptionsToAzureOptionsMapperTestDoubles.MockDefaultMapper();
            var optionsSnapshot =
                SearchSettingsOptionsSnapshotTestDouble
                    .MockForNamedOptions(TargetCollection, options);

            var sut = new SearchOptionsFactory(optionsSnapshot, searchOptionsToAzureOptionsMapperTestDoubles);

            // act
            SearchOptions result = sut.GetSearchOptions(TargetCollection);

            // assert
            result.Should().NotBeNull();
            //Assert.Equal(options, result);
        }

        [Fact]
        public void OptionsDictionary_IsNotSetUpWithCollectioinTypeRequested_ReturnsNull()
        {
            // arrange
            const string TargetCollection = "Establishment";
            const string AlternateTargetCollection = "Mat";
            var options = SearchSettingsOptionsTestDouble.MockFor().Value;
            var searchOptionsToAzureOptionsMapperTestDoubles =
                SearchOptionsToAzureOptionsMapperTestDoubles.MockDefaultMapper();
            var optionsSnapshot =
                SearchSettingsOptionsSnapshotTestDouble
                    .MockForNamedOptions(TargetCollection, options);

            var sut = new SearchOptionsFactory(optionsSnapshot, searchOptionsToAzureOptionsMapperTestDoubles);

            // act
            var result = sut.GetSearchOptions(AlternateTargetCollection);

            // assert
            result.Should().BeNull();
        }

        [Fact]
        public void Options_AreNotSetUp_ReturnsNull()
        {
            // arrange
            const string TargetCollection = "Establishment";
            var optionsSnapshot =
                SearchSettingsOptionsSnapshotTestDouble
                    .MockForNamedOptions(TargetCollection, null!);
            var searchOptionsToAzureOptionsMapperTestDoubles =
                SearchOptionsToAzureOptionsMapperTestDoubles.MockDefaultMapper();

            var sut = new SearchOptionsFactory(optionsSnapshot, searchOptionsToAzureOptionsMapperTestDoubles);

            // act
            var result = sut.GetSearchOptions(TargetCollection);

            // assert
            result.Should().BeNull();
        }
    }
}
