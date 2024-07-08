using Azure.Search.Documents;
using Dfe.Data.SearchPrototype.Infrastructure.Options;
using Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles;
using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;
using Moq;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.Options.TestDoubles
{
    internal static class SearchOptionsToAzureOptionsMapperTestDoubles
    {
        public static IMapper<SearchSettingsOptions, SearchOptions> Dummy() => Mock.Of<IMapper<SearchSettingsOptions, SearchOptions>>();

        public static IMapper<SearchSettingsOptions, SearchOptions> MockDefaultMapper()
        {
            var searchOptionsToAzureOptionsMapperMock = new Mock<IMapper<SearchSettingsOptions, SearchOptions>>();

            searchOptionsToAzureOptionsMapperMock.SetupGet(mapper =>
                mapper.MapFrom(It.IsAny<SearchSettingsOptions>()))
                    .Returns(SearchOptionsFactoryTestDouble.SearchOptionsFake);

            return searchOptionsToAzureOptionsMapperMock.Object;
        }

    }
}
