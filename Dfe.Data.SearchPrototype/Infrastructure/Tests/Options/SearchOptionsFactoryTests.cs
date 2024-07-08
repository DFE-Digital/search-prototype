﻿using Azure.Search.Documents;
using Dfe.Data.SearchPrototype.Infrastructure.Options;
using Dfe.Data.SearchPrototype.Infrastructure.Tests.Options.TestDoubles;
using FluentAssertions;
using Xunit;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.Options
{
    public sealed class SearchOptionsFactoryTests
    {
        [Fact]
        public void GetSearchOptions_ReturnsConfiguredOptions()
        {
            // arrange
            const string TargetCollection = "Establishment";
            var options = SearchSettingsOptionsTestDouble.MockFor().Value;
            var searchOptionsToAzureOptionsMapperTestDoubles =
                SearchOptionsToAzureOptionsMapperTestDoubles.MockDefaultMapper();
            var optionsSnapshot =
                SearchSettingsOptionsSnapshotTestDouble
                    .MockForNamedOptions(TargetCollection, options);

            var searchOptionsFactory =
                new SearchOptionsFactory(optionsSnapshot, searchOptionsToAzureOptionsMapperTestDoubles);

            // act
            SearchOptions? result = searchOptionsFactory.GetSearchOptions(TargetCollection);

            // assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void GetSearchOptions_NoSearchSettingsOptions_ReturnsNull()
        {
            // arrange
            const string TargetCollection = "Establishment";
            var options = SearchSettingsOptionsTestDouble.MockFor().Value;
            var searchOptionsToAzureOptionsMapperTestDoubles =
                SearchOptionsToAzureOptionsMapperTestDoubles.Dummy();
            var optionsSnapshot =
                SearchSettingsOptionsSnapshotTestDouble
                    .MockForNamedOptions(TargetCollection, options);

            var searchOptionsFactory =
                new SearchOptionsFactory(optionsSnapshot, searchOptionsToAzureOptionsMapperTestDoubles);

            // act
            var result = searchOptionsFactory.GetSearchOptions(TargetCollection);

            // assert
            result.Should().BeNull();
        }

        [Fact]
        public void GetSearchOptions_WithNullTargetCollection_ReturnsConfiguredOptions()
        {
            // arrange
            const string TargetCollection = "Establishment";
            var options = SearchSettingsOptionsTestDouble.MockFor().Value;
            var searchOptionsToAzureOptionsMapperTestDoubles =
                SearchOptionsToAzureOptionsMapperTestDoubles.MockDefaultMapper();
            var optionsSnapshot =
                SearchSettingsOptionsSnapshotTestDouble
                    .MockForNamedOptions(TargetCollection, options);

            var searchOptionsFactory =
                new SearchOptionsFactory(optionsSnapshot, searchOptionsToAzureOptionsMapperTestDoubles);

            // act.
            searchOptionsFactory
                .Invoking(searchOptionsFactory =>
                    searchOptionsFactory.GetSearchOptions(targetCollection: null!))
                        .Should()
                            .Throw<ArgumentNullException>()
                            .WithMessage("Value cannot be null. (Parameter 'targetCollection')");
        }
    }
}
