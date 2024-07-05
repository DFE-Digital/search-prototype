using Dfe.Data.SearchPrototype.Search.Domain.Core;

namespace Dfe.Data.SearchPrototype.Search.Domain.AgregateRoot.ValueObjects
{
    /// <summary>
    /// 
    /// </summary>
    public class EstablishmentName : ValueObject<EstablishmentName>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Institution { get; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="institution"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public EstablishmentName(string institution)
        {
            Institution = institution ?? throw new ArgumentNullException(nameof(institution));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<object> GetPropertiesForEqualityCheck()
        {
            yield return Institution;
        }
    }
}
