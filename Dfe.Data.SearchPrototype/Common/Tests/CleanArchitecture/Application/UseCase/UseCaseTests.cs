
using Dfe.Data.SearchPrototype.Common.CleanArchitecture.Application.UseCase;
using Dfe.Data.SearchPrototype.Common.Tests.CleanArchitecture.Application.UseCase.TestDoubles;
using System.Dynamic;
using Xunit;

namespace Dfe.Data.SearchPrototype.Common.Tests.CleanArchitecture.Application.UseCase;

public class UseCaseTests
{
    [Fact]
    public void UseCase_HandleRequest_No_Args_ReturnsExpectedUseCaseReponse()
    {
        // arrange
        var useCaseResponseMessage =
            UseCaseTestDoubles.DefaultUseCaseResponseMessage;
        var useCaseResponsefake =
            UseCaseTestDoubles.DefaultUseCaseResponseFake(useCaseResponseMessage);
        IUseCase<ExpandoObject> useCaseMock =
            UseCaseTestDoubles.MockUseCaseFor(useCaseResponsefake);

        // act
        var useCaseResponse = useCaseMock.HandleRequest();

        // assert
        Assert.Same(
            useCaseResponseMessage,
            useCaseResponse.Result.FirstOrDefault<string>("UseCaseReponse"));
    }

    [Fact]
    public void UseCase_HandleRequest_With_Args_ReturnsExpectedUseCaseReponse()
    {
        // arrange
        var useCaseResponseMessage =
            UseCaseTestDoubles.DefaultUseCaseResponseMessage;
        var useCaseResponsefake =
            UseCaseTestDoubles.DefaultUseCaseResponseFake(useCaseResponseMessage);
        var useCaseRequestMock =
            UseCaseRequestTestDoubles.MockUseCaseRequest<ExpandoObject>();
        IUseCase<IUseCaseRequest<ExpandoObject>, ExpandoObject> useCaseWithArgMock =
            UseCaseTestDoubles.MockUseCaseWithArgFor(useCaseRequestMock, useCaseResponsefake);

        // act
        var useCaseResponse =
            useCaseWithArgMock.HandleRequest(useCaseRequestMock);

        // assert
        Assert.Same(
            useCaseResponseMessage,
            useCaseResponse.Result.FirstOrDefault<string>("UseCaseReponse"));
    }
}

public static class ExpandoObjectExtensions
{
    public static TUnderlyingType? FirstOrDefault<TUnderlyingType>(
        this ExpandoObject expandoObject, string key)
    {
        object? underlyingObject =
            expandoObject.FirstOrDefault(kvp =>
                kvp.Key.Trim() == key.Trim()).Value;

        return (underlyingObject is TUnderlyingType type) ?
            type : default;
    }
}