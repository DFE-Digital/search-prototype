using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dfe.Data.SearchPrototype.SearchForEstablishments;

public class EducationPhase
{

    public string? IsPrimary { get; init; }

    public string? IsSecondary { get; init; }

    public string? IsPost16 { get; init; }

    public EducationPhase()
    { }
    public EducationPhase(string? isPrimary, string? isSecondary, string?isPost16)
    {
        IsPrimary = isPrimary;
        IsSecondary = isSecondary;
        IsPost16 = isPost16;
    }
}
