using AngleSharp.Dom;

namespace DfE.Data.SearchPrototype.Test.PageModels;

public class HomePage : Page
{
    public static string Url = "/";

    public HomePage(IDocument pageResponse) : base(pageResponse)
    {
    }
}
