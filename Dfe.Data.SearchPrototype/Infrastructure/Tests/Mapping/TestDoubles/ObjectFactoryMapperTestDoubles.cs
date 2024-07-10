using DfE.Data.ComponentLibrary.CrossCuttingConcerns.Mapping;
using Moq;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.Mapping.TestDoubles
{
    internal static class ObjectFactoryMapperTestDoubles
    {
        internal static class ObjectFactoryMapperTestDouble
        {
            public static IObjectFactoryMapper Dummy() => Mock.Of<IObjectFactoryMapper>();

            public static IObjectFactoryMapper MockFor<TMapFrom, TMapTo>(TMapTo mapResult)
                where TMapFrom : class
                where TMapTo : class
            {
                var objectFactoryMapper = new Mock<IObjectFactoryMapper>();

                objectFactoryMapper.Setup(_ =>
                    _.Map<TMapFrom, TMapTo>(
                        It.IsAny<TMapFrom>(), It.IsAny<string>()))
                        .Returns(mapResult);

                return objectFactoryMapper.Object;
            }

            public static IObjectFactoryMapper MockWithCallback<TMapFrom, TMapTo>(Action<TMapFrom, string> callbackFunction)
                where TMapFrom : class
                where TMapTo : class
            {
                var objectFactoryMapper = new Mock<IObjectFactoryMapper>();

                objectFactoryMapper.Setup(_ =>
                    _.Map<TMapFrom, TMapTo>(
                        It.IsAny<TMapFrom>(), It.IsAny<string>()))
                        .Callback(callbackFunction);

                return objectFactoryMapper.Object;
            }
        }
    }
}
