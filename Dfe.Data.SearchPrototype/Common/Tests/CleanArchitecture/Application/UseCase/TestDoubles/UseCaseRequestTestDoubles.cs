using Dfe.Data.SearchPrototype.Common.CleanArchitecture.Application.UseCase;
using Moq;

namespace Dfe.Data.SearchPrototype.Common.Tests.CleanArchitecture.Application.UseCase.TestDoubles;

public static class UseCaseRequestTestDoubles
{
    public static Mock<IUseCaseRequest<TUseCaseReponse>> UseCaseRequestMock<
        TUseCaseReponse>() => new();

    public static IUseCaseRequest<TUseCaseReponse> MockUseCaseRequest<
         TUseCaseReponse>() =>
            UseCaseRequestMock<TUseCaseReponse>().Object;
}
