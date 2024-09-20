using Dfe.Data.SearchPrototype.SearchForEstablishments.ByKeyword.Usecase;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.TestDoubles.Shared;

public static class FilterRequestFake
{
    public static FilterRequest Create()
    {
        var faker = new Bogus.Faker();
        return new FilterRequest(faker.Name.JobType(), new List<object>() { faker.Name.JobTitle(), faker.Name.JobTitle() });
    }
}
