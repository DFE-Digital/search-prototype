using Azure.Search.Documents.Models;
using Dfe.Data.SearchPrototype.SearchForEstablishments.Models;
using FluentAssertions;

namespace Dfe.Data.SearchPrototype.Infrastructure.Tests.TestHelpers;

public static class EstablishmentTestExtensionMethods
{
    public static void ShouldHaveMatchingMappedEstablishment(this SearchResult<DataTransferObjects.Establishment> searchResult, EstablishmentResults mappedEstablishments)
    {
        var mappedEstablishment =
            mappedEstablishments.Establishments.ToList()
                .Find(establishment =>
                    establishment.Urn == searchResult.Document.id);
        
        mappedEstablishment!.Name
            .Should().Be(searchResult.Document.ESTABLISHMENTNAME);
    }
}
