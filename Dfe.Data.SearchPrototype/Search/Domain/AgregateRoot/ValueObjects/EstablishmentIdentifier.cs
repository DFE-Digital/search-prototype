using Dfe.Data.SearchPrototype.Search.Domain.Core;

namespace Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.ValueObjects
{
    /// <summary>
    /// 
    /// </summary>
    public class EstablishmentIdentifier : ValueObject<EstablishmentIdentifier>
    {
        /// <summary>
        /// 
        /// </summary>
        public string URN { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="urn"></param>
        public EstablishmentIdentifier(string urn)
        {
            if (string.IsNullOrWhiteSpace(urn))
            {
                throw new ArgumentNullException(nameof(urn));
            }

            URN = urn;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override IEnumerable<object> GetPropertiesForEqualityCheck()
        {
            yield return URN;
        }
    }
}