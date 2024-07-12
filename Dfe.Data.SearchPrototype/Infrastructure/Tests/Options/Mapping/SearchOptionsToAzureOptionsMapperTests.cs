using Dfe.Data.SearchPrototype.Infrastructure.Options;
using Dfe.Data.SearchPrototype.Infrastructure.Options.Mapping;
using Dfe.Data.SearchPrototype.Infrastructure.Tests.Options.TestDoubles;
using FluentAssertions;
using Xunit;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.Options.Mapping;

public sealed class SearchOptionsToAzureOptionsMapperTests
{
    [Fact]
    public void MapFrom_With_Valid_Search_Settings_Options_Returns_Search_Options()
    {
        // arrange
        SearchSettingsOptions options = SearchSettingsOptionsTestDouble.MockFor().Value;

        // act
        var result = new SearchOptionsToAzureOptionsMapper().MapFrom(options);

        // assert
        result.Should().NotBeNull();
    }

    [Fact]
    public void MapFrom_With_Null_Search_Settings_Options_Throws_Expected_ArgumentNullException()
    {
        // arrange
        var mapper = new SearchOptionsToAzureOptionsMapper();

        // assert
        mapper
            .Invoking(mapper =>
                mapper.MapFrom(input: null!))
                    .Should()
                        .Throw<ArgumentNullException>()
                        .WithMessage("Value cannot be null. (Parameter 'input')");
    }
}
