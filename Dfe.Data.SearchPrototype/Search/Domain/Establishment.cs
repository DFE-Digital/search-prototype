using Dfe.Data.SearchPrototype.Search.Domain.Core.DfE.ComponentLibrary.DomainDrivenDesign.Domain;

namespace Dfe.Data.SearchPrototype.Search.Domain
{
    public sealed class Establishment : Entity<EstablishmentIdentifier>
    {
        public Establishment(EstablishmentIdentifier establishmentIdentifier) : base(establishmentIdentifier)
        {
        }
    }
}
