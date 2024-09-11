using Azure.Search.Documents.Models;
using Dfe.Data.SearchPrototype.Infrastructure.DataTransferObjects;
using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;
using FluentAssertions;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.TestHelpers;

public static class EstablishmentTestExtensionMethods
{
    public static void ShouldHaveMatchingMappedEstablishment(this SearchResult<Establishment> searchResult, EstablishmentResults mappedEstablishments)
    {
        var mappedEstablishment = mappedEstablishments.Establishments.ToList().Find(x => x.Urn == searchResult.Document.id);
        mappedEstablishment!.Name.Should().Be(searchResult.Document.ESTABLISHMENTNAME);
    }
}
