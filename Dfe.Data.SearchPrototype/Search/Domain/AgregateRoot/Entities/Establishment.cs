using Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.ValueObjects;
using Dfe.Data.SearchPrototype.Search.Domain.Core.DfE.ComponentLibrary.DomainDrivenDesign.Domain;

namespace Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Establishment : Entity<EstablishmentIdentifier>
    {
        public EstablishmentName Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="establishmentIdentifier"></param>
        public Establishment(
            EstablishmentIdentifier establishmentIdentifier, EstablishmentName name)
                : base(establishmentIdentifier)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
