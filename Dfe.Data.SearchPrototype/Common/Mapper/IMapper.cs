namespace Dfe.Data.SearchPrototype.Common.Mappers;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TMapFrom"></typeparam>
/// <typeparam name="TMapTo"></typeparam>
public interface IMapper<in TMapFrom, out TMapTo>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="input">
    /// 
    /// </param>
    /// <returns></returns>
    TMapTo MapFrom(TMapFrom input);
}
