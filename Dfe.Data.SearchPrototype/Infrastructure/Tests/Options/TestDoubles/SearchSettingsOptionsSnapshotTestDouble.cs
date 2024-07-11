using Dfe.Data.SearchPrototype.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Moq;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.Options.TestDoubles;

internal static class SearchSettingsOptionsSnapshotTestDouble
{
    public static IOptionsSnapshot<SearchSettingsOptions> MockForNamedOptions(string optionName, SearchSettingsOptions options)
    {
        var optionsSnapshot = new Mock<IOptionsSnapshot<SearchSettingsOptions>>();
        optionsSnapshot.Setup(searchSettingsOptions =>
            searchSettingsOptions.Get(optionName))
                .Returns(options);
        
        return optionsSnapshot.Object;
    }
}
