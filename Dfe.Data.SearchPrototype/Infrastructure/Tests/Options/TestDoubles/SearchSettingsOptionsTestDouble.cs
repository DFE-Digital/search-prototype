using Dfe.Data.SearchPrototype.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Moq;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.Options.TestDoubles
{
    internal static class SearchSettingsOptionsTestDouble
    {
        public static IOptions<SearchSettingsOptions> Dummy() => Mock.Of<IOptions<SearchSettingsOptions>>();

        public static IOptions<SearchSettingsOptions> MockFor()
        {
            var searchSettingsOptionsMock = new Mock<IOptions<SearchSettingsOptions>>();
            var searchSettingsOptions = new SearchSettingsOptions() { SearchIndex = "TestIndex" };

            searchSettingsOptionsMock.SetupGet(options =>
                options.Value).Returns(searchSettingsOptions);

            return searchSettingsOptionsMock.Object;
        }
    }
}
