using AngleSharp.Dom;

namespace DfE.Data.SearchPrototype.Test.PageModels;

public class PrivacyPage : Page
{
    public static string Url = "/Home/Privacy";

    public PrivacyPage(IDocument pageResponse) : base(pageResponse)
    {
    }
}
