namespace Dfe.Data.SearchPrototype.SearchForEstablishments.Models;

/// <summary>
/// Object used to encapsulate the education phase of the retrieved establishment.
/// </summary>
public class EducationPhase
{
    /// <summary>
    /// true if the establishment includes the Primary phase of education
    /// false if the establishment does not include the Primary phase of education
    /// </summary>
    public bool IsPrimary { get; }

    /// <summary>
    /// true if the establishment includes the secondary phase of education
    /// false if the establishment does not include the secondary phase of education 
    /// </summary>
    public bool IsSecondary { get; }

    /// <summary>
    /// true if the establishment includes the post 16 phase of education
    /// false if the establishment does not include the post 16 phase of education 
    /// </summary>
    public bool IsPost16 { get; }

    /// <summary>
    /// Establishes an immutable education phase of establishment instance via the constructor arguments specified.
    /// </summary>
    ///<param name="isPrimary">
    /// "1" if the establishment includes the Primary phase of education
    /// "0" if the establishment does not include the Primary phase of education
    /// </param>
    /// <param name="isSecondary">
    /// "1" if the establishment includes the secondary phase of education
    /// "0" if the establishment does not include the secondary phase of education
    /// </param>
    /// <param name="isPost16">
    /// "1" if the establishment includes the post 16 phase of education
    /// "0" if the establishment does not include the post 16 phase of education 
    /// </param>
    public EducationPhase(string isPrimary, string isSecondary, string isPost16)
    {
        IsPrimary = isPrimary == "1" ? true : false;
        IsSecondary = isSecondary == "1" ? true : false;
        IsPost16 = isPost16 == "1" ? true : false;
    }
}