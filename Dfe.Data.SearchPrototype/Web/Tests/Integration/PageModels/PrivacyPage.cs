using AngleSharp.Dom;
using DfE.Data.SearchPrototype.Test.PageModels;

namespace DfE.Data.SearchPrototype.Web.Tests.Integration.PageModels;

public class PrivacyPage : Page
{
    public static string Url = "/Home/Privacy";

    public PrivacyPage(IDocument pageResponse) : base(pageResponse)
    {
    }
}
