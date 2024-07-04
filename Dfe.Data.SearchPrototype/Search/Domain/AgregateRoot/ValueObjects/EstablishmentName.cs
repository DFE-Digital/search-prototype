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
        public string Title { get; private set; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public EstablishmentName(string title)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<object> GetPropertiesForEqualityCheck()
        {
            yield return Title;
        }
    }
}
