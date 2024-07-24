using Dfe.Data.SearchPrototype.Common.CleanArchitecture.Application.UseCase;
using Moq;
using System.Dynamic;
using Bogus;

namespace Dfe.Data.SearchPrototype.Common.Tests.CleanArchitecture.Application.UseCase.TestDoubles;

public static class UseCaseTestDoubles
{
    public static Mock<IUseCase<TUseCaseReponseMock>> UseCaseMock<
        TUseCaseReponseMock>() => new();

    public static Mock<IUseCase<IUseCaseRequest<
        TUseCaseReponse>, TUseCaseReponse>> UseCaseMockWithArg<TUseCaseReponse>() => new();

    public static IUseCase<TUseCaseReponseMock> MockUseCaseFor<
        TUseCaseReponseMock>(TUseCaseReponseMock useCaseReponse)
    {
        var useCaseMock = UseCaseMock<TUseCaseReponseMock>();

        useCaseMock
            .Setup(useCase =>
                useCase.HandleRequest())
                    .Returns(MockUseCaseResponseFor(useCaseReponse));

        return useCaseMock.Object;
    }

    public static IUseCase<IUseCaseRequest<TUseCaseReponse>, TUseCaseReponse> MockUseCaseWithArgFor<
       TUseCaseReponse>(IUseCaseRequest<TUseCaseReponse> useCaseRequest, TUseCaseReponse useCaseReponse)
    {
        var useCaseWithArgMock = UseCaseMockWithArg<TUseCaseReponse>();

        useCaseWithArgMock
            .Setup(useCase =>
                useCase.HandleRequest(useCaseRequest))
                    .Returns(MockUseCaseResponseFor(useCaseReponse));

        return useCaseWithArgMock.Object;
    }

    public static Task<TUseCaseResponse> MockUseCaseResponseFor<
        TUseCaseResponse>(TUseCaseResponse useCaseResponseFake) =>
            Task.FromResult(useCaseResponseFake);

    public static ExpandoObject DefaultUseCaseResponseFake<TUseCaseResponse>(
        TUseCaseResponse useCaseResponse)
    {
        dynamic useCaseResponsefake = new ExpandoObject();
        useCaseResponsefake.UseCaseReponse = useCaseResponse;
        return useCaseResponsefake;
    }

    public static string DefaultUseCaseResponseMessage => new Faker().Random.Words(count: 10);
}
