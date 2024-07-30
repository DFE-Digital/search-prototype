namespace Dfe.Data.SearchPrototype.SearchForEstablishments;

public class EducationPhase
{
    public bool IsPrimary { get; }

    public bool IsSecondary { get; }

    public bool IsPost16 { get; }

    public EducationPhase(string isPrimary, string isSecondary, string isPost16)
    {
        IsPrimary = isPrimary == "1" ? true : false;
        IsSecondary = isSecondary == "1" ? true : false;
        IsPost16 = isPost16 == "1" ? true : false;
    }
}